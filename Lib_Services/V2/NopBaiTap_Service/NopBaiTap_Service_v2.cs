using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Update.BaiTap;
using Lib_Models.Models_Insert.v2.BaiTap;
using Lib_Models.Models_Select.BaiTap;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Repository.Abstract;
using Lib_Services.Token_Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.NopBaiTap_Service
{
    public class NopBaiTap_Service_v2 : INopBaiTap_Service_v2
    {
        private readonly Trendyt_DbContext _db;
        private readonly IToken_Service_v2 _token_Service_V2;
        private readonly IRepository<tbKhoaSchool> _repositoryKhoaSchool;
        private readonly IRepository<tbBaiTap> _repositoryBaiTap;
        private readonly IRepository<tbNopBaiTap> _repositoryNopBaiTap;
        private readonly IRepository<tbFileNopBaiTap> _repositoryFileNopBaiTap;
        private readonly IRepository<tbMonHocClass_Student> _repositoryMonHocClass_Student;
        public NopBaiTap_Service_v2(IToken_Service_v2 token_Service_V2 , IRepository<tbKhoaSchool> repositoryKhoaSchool, IRepository<tbNopBaiTap> repositoryNopBaiTap,
            IRepository<tbFileNopBaiTap> repositoryFileNopBaiTap, IRepository<tbMonHocClass_Student> repositoryMonHocClass_Student, IRepository<tbBaiTap> repositoryBaiTap,
            Trendyt_DbContext db)
        {
            _db = db;
            _token_Service_V2 = token_Service_V2;
            _repositoryKhoaSchool = repositoryKhoaSchool;
            _repositoryNopBaiTap = repositoryNopBaiTap;
            _repositoryFileNopBaiTap = repositoryFileNopBaiTap;
            _repositoryMonHocClass_Student = repositoryMonHocClass_Student;
            _repositoryBaiTap = repositoryBaiTap;
        }
       
        public async Task<Status_Application> Add(NopBaiTap_Insert_v2 baiTap)
        {
            try
            {
                tbMenberSchool memberManager = await _token_Service_V2.Get_Menber_Token();
                var baiTapDb = await _repositoryBaiTap.GetAll(x => x.idBaiTap == baiTap.idBaiTap);
                if (!baiTapDb.Any())
                {
                    return new Status_Application { StatusBool = false, StatusType = "Không tìm thấy id bài tập" };
                }
                var baiTapFist = baiTapDb.First();
                var monHoc_Students = await _repositoryMonHocClass_Student.GetAll(x => x.id_MonHoc == baiTapFist.id_MonHoc 
                    && x.id_MenberSchool == memberManager.id_MenberSchool);
                if (!monHoc_Students.Any())
                {
                    return new Status_Application { StatusBool = false, StatusType = "Sinh viên không thuộc môn này" };
                }
                var monHoc_Student = monHoc_Students.First();
                tbNopBaiTap nopBaiTap = new tbNopBaiTap
                {
                    idBaiTap = baiTapFist.idBaiTap,
                    id_MonHocClass_Student = monHoc_Student.id_MonHocClass_Student,
                    diem = 0,
                    danhGia = "",
                    createTime = DateTime.Now,
                };

                await _repositoryNopBaiTap.Insert(nopBaiTap);
                await _repositoryNopBaiTap.Commit();

                // Path: wwwroot/baitap/idKhoa/idMonHoc/idBaiTap/nopbaitap/file.file
                List<string> path = new List<string>
                {
                        "baitap", memberManager.id_KhoaSchool.ToString(), baiTapFist.id_MonHoc.ToString(), baiTapFist.idBaiTap.ToString() , "nopbaitap"
                };
                if (baiTap.files!.Count() > 0)
                {
                    await AddFileNopBaiTap(baiTap.files!, nopBaiTap.idNopBaiTap, path);
                }
                return new Status_Application { StatusBool = true, List_String_Int = path };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false , StatusType = ex.Message};
            }
            throw new NotImplementedException();
        }

        public async Task ChamDiem(ChamDiemModel chamDiem)
        {
            var baiTap = await _repositoryNopBaiTap.GetById(chamDiem.idNopBaiTap);
            baiTap.diem = chamDiem.diem;
            baiTap.danhGia = chamDiem.danhGia;
            await _repositoryNopBaiTap.Commit();
        }

        public async Task<List<BaiTapNopModelSelect>> GetAll(int idBaiTap)
        {
            tbMenberSchool memberManager = await _token_Service_V2.Get_Menber_Token();
            var monHoc_Student = await _repositoryMonHocClass_Student.GetAll(x => x.id_MenberSchool == memberManager.id_MenberSchool);
            int idStudent = monHoc_Student.First().id_MonHocClass_Student;

            var query = await _repositoryNopBaiTap.GetAllIncluding(x => x.idBaiTap == idBaiTap);

            if (memberManager.tbRoleSchool!.name_Role == "student")
            {
                query = query.Where(x => x.id_MonHocClass_Student == idStudent);
            }

            var data = query.ToList();

            var dataRl = data.Select(x => new BaiTapNopModelSelect
            {
                idBaiTap = x.idBaiTap,
                idNopBaiTap = x.idNopBaiTap,
                diem = x.diem,
                danhGia = x.danhGia,
                createTime = x.createTime,
                student = (from cm in _db.tbMonHocClass_Student
                           where cm.id_MonHocClass_Student == x.id_MonHocClass_Student
                           join m in _db.tbMenberSchool
                           on cm.id_MenberSchool equals m.id_MenberSchool
                           join ac in _db.tbAccount 
                           on m.id_Account equals ac.id_Account
                           select ac.fullName).FirstOrDefault(),
                file = (from f in _db.tbFileNopBaiTap
                        where f.idNopBaiTap == x.idNopBaiTap
                        select new tbFileNopBaiTap
                        {
                            file = f.file,
                            idFileNopBaiTap = f.idNopBaiTap,
                        }).ToList()
            });

            return dataRl!.ToList();
        }

        private async Task AddFileNopBaiTap(List<IFormFile> files, int idNopBaiTap, List<string> path)
        {
            try
            {
                List<tbFileNopBaiTap> fileBaiTaps = new List<tbFileNopBaiTap>();
                string patgSrc = path[0] + "/" + path[1] + "/" + path[2] + "/" + path[3] + "/" + path[4] + "/";
                foreach (var file in files)
                {
                    tbFileNopBaiTap fileBaiTap = new tbFileNopBaiTap
                    {
                        file = patgSrc + file.FileName,
                        idNopBaiTap = idNopBaiTap
                    };
                    fileBaiTaps.Add(fileBaiTap);
                }

                await _repositoryFileNopBaiTap.Insert(fileBaiTaps);
                await _repositoryBaiTap.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
