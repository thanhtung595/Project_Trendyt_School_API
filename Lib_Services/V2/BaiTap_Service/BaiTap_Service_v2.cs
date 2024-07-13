using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
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

namespace Lib_Services.V2.BaiTap_Service
{
    public class BaiTap_Service_v2 : IBaiTap_Service_v2
    {
        private readonly Trendyt_DbContext _db;
        private readonly IRepository<tbBaiTap> _repositoryBaiTap;
        private readonly IRepository<tbLichHoc> _repositoryLichHoc;
        private readonly IRepository<tbMonHoc> _repositoryMonHoc;
        private readonly IRepository<tbMonHocClass_Student> _repositoryMonHocClass_Student;
        private readonly IRepository<tbKhoaSchool> _repositoryKhoaSchool;
        private readonly IRepository<tbFileBaiTap> _repositoryFileBaiTap;
        private readonly IToken_Service_v2 _token_Service_V2;
        public BaiTap_Service_v2(IRepository<tbBaiTap> repositoryBaiTap, IRepository<tbFileBaiTap> repositoryFileBaiTap, IToken_Service_v2 token_Service_V2, Trendyt_DbContext db,
            IRepository<tbKhoaSchool> repositoryKhoaSchool, IRepository<tbLichHoc> repositoryLichHoc, IRepository<tbMonHoc> repositoryMonHoc, IRepository<tbMonHocClass_Student> repositoryMonHocClass_Student)
        {
            _repositoryBaiTap = repositoryBaiTap;
            _db = db;
            _repositoryFileBaiTap = repositoryFileBaiTap;
            _token_Service_V2 = token_Service_V2;
            _repositoryKhoaSchool = repositoryKhoaSchool;
            _repositoryLichHoc = repositoryLichHoc;
            _repositoryMonHoc = repositoryMonHoc;
            _repositoryMonHocClass_Student = repositoryMonHocClass_Student;
        }
        public async Task<Status_Application> Add(BaiTap_Insert_v2 baiTap)
        {
            try
            {
                if (string.IsNullOrEmpty(baiTap.nameBaiTap))
                {
                    return new Status_Application { StatusBool = false, StatusType = "Chưa nhập tên bài tập"};
                }
                tbMenberSchool memberManager = await _token_Service_V2.Get_Menber_Token();
                var baiTapAdd = new tbBaiTap
                {
                    nameBaiTap = baiTap.nameBaiTap,
                    moTa = baiTap.moTa,
                    hanNopBai = baiTap.hanNopBai ?? DateTime.MinValue,
                    createTime = DateTime.Now,
                    id_MonHoc = baiTap.id_MonHoc,
                    id_MenberSchool = memberManager.id_MenberSchool,
                };
                await _repositoryBaiTap.Insert(baiTapAdd);
                await _repositoryBaiTap.Commit();
                var khoa = await _repositoryKhoaSchool.GetById(memberManager.id_KhoaSchool);
                string nameKhoa = khoa.id_KhoaSchool.ToString();

                // Path: wwwroot/baitap/idKhoa/idMonHoc/idBaiTap/baitap/file.file
                List<string> path = new List<string>
                {
                        "baitap", nameKhoa, baiTap.id_MonHoc.ToString(), baiTapAdd.idBaiTap.ToString() , "baitap"
                };
                if (baiTap.files!.Count() > 0)
                {
                    
                    await AddFileBaiTap(baiTap.files!, baiTapAdd, path);
                }
                
                return new Status_Application { StatusBool = true , List_String_Int = path };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false , StatusType = ex.Message };
            }
        }

        public async Task<List<BaiTapModelSelecAll>> GetAll(int idMonHoc)
        {
            tbMenberSchool memberManager = await _token_Service_V2.Get_Menber_Token();

            var roleString = memberManager.tbRoleSchool!.name_Role;

            var menber_MonHocs = await _repositoryMonHocClass_Student.GetAll(x => x.id_MenberSchool == memberManager.id_MenberSchool);
            if (!menber_MonHocs.Any())
            {
                return null!;
            }
            var menber_MonHoc = menber_MonHocs.First();
            var baiTaps = await _repositoryBaiTap.GetAll();
            if (idMonHoc > 0)
            {
                baiTaps = baiTaps.Where(x => x.id_MonHoc == idMonHoc);
            }

            var dataTasks = baiTaps.Select(x => new BaiTapModelSelecAll
            {
                idBaiTap = x.idBaiTap,
                nameBaiTap = x.nameBaiTap,
                moTa = x.moTa,
                hanNopBai = x.hanNopBai,
                createTime = x.createTime,
                id_MonHoc = x.id_MonHoc,
                file = (from f in _db.tbFileBaiTap
                        where f.idBaiTap == x.idBaiTap
                        select new FileBaiTapModel_Select
                        {
                            urlFile = f.file,
                            nameFile = f.file!.Substring(f.file.LastIndexOf('/') + 1),
                            idFile = f.idFileBaiTap,
                        }).ToList(),
                giaoVienGiao = (from m in _db.tbMenberSchool
                                join ac in _db.tbAccount
                                on m.id_Account equals ac.id_Account
                                where m.id_MenberSchool == x.id_MenberSchool
                                select ac.fullName).FirstOrDefault(),
                isDaNopBai = (roleString == "student" ?
                                (from bt in _db.tbNopBaiTap
                                 where bt.idBaiTap == x.idBaiTap && bt.id_MonHocClass_Student == menber_MonHoc.id_MonHocClass_Student
                                 select bt).Any() : false
                              )
            }).ToList();
            return dataTasks;
        }

        private async Task AddFileBaiTap(List<IFormFile> files, tbBaiTap baiTap, List<string> path)
        {
            try
            {
                List<tbFileBaiTap> fileBaiTaps = new List<tbFileBaiTap>();
                string patgSrc = path[0]+"/"+ path[1] + "/" + path[2] + "/" + path[3] + "/" + path[4] + "/";
                foreach (var file in files)
                {
                    tbFileBaiTap fileBaiTap = new tbFileBaiTap
                    {
                        file = patgSrc + file.FileName,
                        idBaiTap = baiTap.idBaiTap
                    };
                    fileBaiTaps.Add(fileBaiTap);
                }

                await _repositoryFileBaiTap.Insert(fileBaiTaps);
                await _repositoryBaiTap.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //private string TypeFile(IFormFile file)
        //{
        //    IFormFile fileAdd = file;
        //    string fileNameRequest = fileAdd.FileName;
        //    int lenghtFileNameRequest = fileNameRequest.Length;
        //    int lastIndexOfDot = fileNameRequest.LastIndexOf('.');
        //    int count_Cut_FrommatFile = lenghtFileNameRequest - lastIndexOfDot;
        //    string text_Cut_FrommatFile = fileNameRequest.Substring(lastIndexOfDot + 1, count_Cut_FrommatFile - 1);
        //    return "." + text_Cut_FrommatFile!;
        //}
    }
}
