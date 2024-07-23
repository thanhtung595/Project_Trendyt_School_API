using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Azure.Core;
using Lib_Models.Model_Update.DiemDanh;
using Lib_Models.Models_Select.DiemDanh;
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
        private readonly IRepository<tbMonHoc> _repositoryMonHoc;
        private readonly IRepository<tbLichHoc> _repositoryLichHoc;
        public DiemDanh_Service_v2(IRepository<tbDiemDanh> repositoryDiemDanh, IRepository<tbMonHoc> repositoryMonHoc, Trendyt_DbContext db,
            IRepository<tbLichHoc> repositoryLichHoc)
        {
            _repositoryDiemDanh = repositoryDiemDanh;
            _repositoryMonHoc = repositoryMonHoc;
            _db = db;
            _repositoryLichHoc = repositoryLichHoc;
        }

        //public async Task<List<IGrouping<DateTime, DiemDanh_Select_v1>>> GetDiemDanhMonHocAsync(int idMonHoc)
        //{
        //    var query = await _repositoryDiemDanh.GetAllIncluding(dd => dd.tbLichHoc!.id_MonHoc == idMonHoc, ms => ms.tbMonHocClass_Student!.tbMenberSchool!.tbAccount!);
        //    if (!query.Any())
        //    {
        //        return null!;
        //    }

        //    var result = query.Select(d => new DiemDanh_Select_v1
        //    {
        //        ThoiGianBatDau = (from x in _db.tbLichHoc
        //                          where x.id_LichHoc == d.id_LichHoc
        //                          select x.thoiGianBatDau).FirstOrDefault(),
        //        Students = new List<DiemDanhStudent_Select>
        //        {
        //            new DiemDanhStudent_Select
        //            {
        //                id_DiemDanh = d.id_DiemDanh,
        //                id_LichHoc = d.id_LichHoc
        //            }
        //        }
        //    }).ToList();

        //    //Nhóm các result với id_LichHoc
        //    var groupResult = result.GroupBy(d => d.ThoiGianBatDau).ToList();
        //    return groupResult;
        //}
        public async Task<List<DiemDanh_Select_v1>> GetDiemDanhMonHocAsync(int idMonHoc)
        {
            var lichHoc = await _repositoryLichHoc.GetAll(x => x.id_MonHoc == idMonHoc);
            if (!lichHoc.Any())
            {
                return null!;
            }
            var result = lichHoc.Select(d => new DiemDanh_Select_v1
            {
                ThoiGianBatDau = d.thoiGianBatDau,
                Students = (from s in _db.tbDiemDanh
                            join sm in _db.tbMonHocClass_Student
                            on s.id_MonHocClass_Student equals sm.id_MonHocClass_Student
                            join m in _db.tbMenberSchool
                            on sm.id_MenberSchool equals m.id_MenberSchool
                            join ac in _db.tbAccount
                            on m.id_Account equals ac.id_Account
                            where s.id_LichHoc == d.id_LichHoc
                            select new DiemDanhStudent_Select
                            {
                                id_DiemDanh = s.id_DiemDanh,
                                id_LichHoc = s.id_LichHoc,
                                _CuoiGio = s._CuoiGio,
                                _DauGio = s._DauGio,
                                msv = m.id_MenberSchool,
                                fullName = ac.fullName
                            }).ToList(),
            }).ToList();
            return result;
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

        public async Task<Status_Application> UpdateAsync(List<LopDiemDanh_Update_v1> request)
        {
            try
            {
                foreach (var item in request)
                {
                    var diemDanh = await _repositoryDiemDanh.GetById(item.id_DiemDanh);
                    if (diemDanh == null!)
                    {
                        return new Status_Application
                        {
                            StatusBool = false,
                            StatusType = "error id điểm danh"
                        };
                    }

                    diemDanh._DauGio = item._DauGio;
                    diemDanh._CuoiGio = item._CuoiGio;
                    diemDanh._DiMuon = item._DiMuon;
                    diemDanh.editLastTime = DateTime.Now;
                    
                    _repositoryDiemDanh.Update(diemDanh);
                }
                await _repositoryDiemDanh.Commit();
                return new Status_Application
                {
                    StatusBool = true,
                    StatusType = "success"
                };
            }
            catch (Exception ex)
            {
                return new Status_Application
                {
                    StatusBool = false,
                    StatusType = ex.Message
                };
            }
        }
    }
}
