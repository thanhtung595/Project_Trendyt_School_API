using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Update.DiemDanh;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.DiemDanh
{
    public class DiemDanh_Service_v2 : IDiemDanh_Service_v2
    {
        private readonly Trendyt_DbContext _db;
        private readonly IRepository<tbDiemDanh> _repositoryDiemDanh;
        private readonly IRepository<tbMenberSchool> _repositoryMenberSchool;
        private readonly IRepository<tbMonHocClass_Student> _repositoryMonHocClass_Student;
        private readonly IRepository<tbMonHoc> _repositoryMonHoc;
        public DiemDanh_Service_v2(IRepository<tbDiemDanh> repositoryDiemDanh)
        {
            _repositoryDiemDanh = repositoryDiemDanh;
        }

        public async Task<Status_Application> InsertAsync(List<int> idLichHoc, List<int> idStudent)
        {
            try
            {
                List<tbDiemDanh> addDiemDanhs = new List<tbDiemDanh>();

                foreach (var lichHoc in idLichHoc)
                {
                    foreach (var sudent in idStudent)
                    {
                        tbDiemDanh addDiemDanh = new tbDiemDanh
                        {
                            id_LichHoc = lichHoc,
                            id_MonHocClass_Student = sudent,
                            _DauGio = false,
                            _CuoiGio = false,
                            _DiMuon = false,
                        };
                        addDiemDanhs.Add(addDiemDanh);
                    }
                }

                await _repositoryDiemDanh.Insert(addDiemDanhs);
                await _repositoryDiemDanh.Commit();

                return new Status_Application { StatusBool = true, StatusType = "success" };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false , StatusType = ex.Message};
            }
        }

        public async Task<Status_Application> UpdateAsync(LopDiemDanh_Update_v1 request)
        {
            try
            {
                // Kiểm tra môn học có tồn tại
                var monHocDb = await _repositoryMonHoc.GetById(request.id_MonHoc);
                if (monHocDb == null)
                {
                    return new Status_Application { StatusBool = false, StatusType = "Môn học không tồn tại" };
                }

                // Kiểm tra student có tồn tại trong danh sách
                foreach (var student in request.students!)
                {
                    var isStudent = await _db.tbMonHocClass_Student.FirstOrDefaultAsync(x => x.id_MonHoc == request.id_MonHoc
                                                                                        && x.id_MonHocClass_Student == student.msv);
                    if (isStudent == null)
                    {
                        return new Status_Application { StatusBool = false, StatusType = $"msv {student.msv} không trong lớp này." };
                    }
                }
                List<tbDiemDanh> updateDiemDanhs = new List<tbDiemDanh>();
                foreach (var student in request.students!)
                {
                    tbDiemDanh updateDiemDanh = new tbDiemDanh
                    {
                        
                    };
                }
                return null!;
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false, StatusType = ex.Message };
            }
        }
    }
}
