using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Insert.v1.MonHoc;
using Lib_Models.Models_Select.LichHoc;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.LichHoc_Repository
{
    public class LichHoc_Repository_v1 : ILichHoc_Repository_v1
    {
        private readonly Trendyt_DbContext _db;
        public LichHoc_Repository_v1(Trendyt_DbContext db)
        {
            _db = db;
        }
        public async Task<Status_Application> Insert(LichHoc_Insert_v1 lichHoc)
        {
            try
            {
                tbLichHoc tbLichHoc = new tbLichHoc
                {
                    id_MonHoc = lichHoc.id_MonHoc,
                    thoiGianBatDau = lichHoc.thoiGianBatDau,
                    thoiGianKetThuc = lichHoc.thoiGianKetThuc,
                    phonghoc = lichHoc.phonghoc,
                };
                await _db.tbLichHoc.AddAsync(tbLichHoc);
                await _db.SaveChangesAsync();
                return new Status_Application { StatusBool = true, StatusType = "success" };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false , StatusType = "error"+ex.Message };
                throw;
            }
        }

        public async Task<List<LichHoc_Select_All_v1>> SelectAll(tbMenberSchool menberSchool)
        {
            IQueryable<tbMonHocClass_Student> queryRole = _db.tbMonHocClass_Student;
            if (menberSchool.tbRoleSchool!.name_Role == "teacher" || menberSchool.tbRoleSchool.name_Role == "student")
            {
                queryRole = queryRole.Where(x => x.id_MenberSchool == menberSchool.id_MenberSchool);
                return await StudentTeacher(menberSchool, queryRole);
            }
            else
            {
                return await AdminGet(menberSchool);
            }
            
        }

        private async Task<List<LichHoc_Select_All_v1>> StudentTeacher(tbMenberSchool menberSchool, IQueryable<tbMonHocClass_Student> queryRole)
        {
            var query = await (from mb in queryRole
                               join monhoc in _db.tbMonHoc
                               on mb.id_MonHoc equals monhoc.id_MonHoc
                               where monhoc.id_School == menberSchool.id_School
                               join lichhoc in _db.tbLichHoc
                               on monhoc.id_MonHoc equals lichhoc.id_MonHoc
                               select new
                               {
                                   Date = lichhoc.thoiGianBatDau.Date, // Lấy ra ngày tháng năm của thời gian bắt đầu,
                                   MonHoc = monhoc.name_MonHoc,
                                   Teacher = (from mh_tc in _db.tbMonHocClass_Student
                                              where mh_tc.id_MonHoc == monhoc.id_MonHoc
                                              join menber in _db.tbMenberSchool
                                              on mh_tc.id_MenberSchool equals menber.id_MenberSchool
                                              join role in _db.tbRoleSchool
                                              on menber.id_RoleSchool equals role.id_RoleSchool
                                              where role.name_Role == "teacher"
                                              join ac in _db.tbAccount
                                              on menber.id_Account equals ac.id_Account
                                              select ac.fullName).FirstOrDefault(),
                                   IdLichHoc = lichhoc.id_LichHoc,
                                   ThoiGianBatDau = lichhoc.thoiGianBatDau.TimeOfDay, // Lấy giờ, phút, giây của thời gian bắt đầu
                                   ThoiGianKetThuc = lichhoc.thoiGianKetThuc.TimeOfDay, // Lấy giờ, phút, giây của thời gian kết thúc
                                   PhongHoc = lichhoc.phonghoc
                               }).ToListAsync();

            var groupByNgayThoiGian = query.GroupBy(x => x.Date)
                                            .Select(g => new LichHoc_Select_All_v1
                                            {
                                                date = g.Key.ToString("yyyy-MM-dd"),
                                                lichmon = g.Select(item => new LichHoc_Select
                                                {
                                                    teacher = item.Teacher,
                                                    MonHoc = item.MonHoc,
                                                    id_LichHoc = item.IdLichHoc,
                                                    thoiGianBatDau = item.ThoiGianBatDau.ToString(),
                                                    thoiGianKetThuc = item.ThoiGianKetThuc.ToString(),
                                                    phonghoc = item.PhongHoc
                                                }).ToList()
                                            })
                                            .ToList();
            return groupByNgayThoiGian;
        }

        private async Task<List<LichHoc_Select_All_v1>> AdminGet(tbMenberSchool menberSchool)
        {
            var query = await (from monhoc in _db.tbMonHoc
                               where monhoc.id_School == menberSchool.id_School
                               join lichhoc in _db.tbLichHoc
                               on monhoc.id_MonHoc equals lichhoc.id_MonHoc
                               select new
                               {
                                   Date = lichhoc.thoiGianBatDau.Date, // Lấy ra ngày tháng năm của thời gian bắt đầu,
                                   MonHoc = monhoc.name_MonHoc,
                                   Teacher = (from mh_tc in _db.tbMonHocClass_Student
                                              where mh_tc.id_MonHoc == monhoc.id_MonHoc
                                              join menber in _db.tbMenberSchool
                                              on mh_tc.id_MenberSchool equals menber.id_MenberSchool
                                              join role in _db.tbRoleSchool
                                              on menber.id_RoleSchool equals role.id_RoleSchool
                                              where role.name_Role == "teacher"
                                              join ac in _db.tbAccount
                                              on menber.id_Account equals ac.id_Account
                                              select ac.fullName).FirstOrDefault(),
                                   IdLichHoc = lichhoc.id_LichHoc,
                                   ThoiGianBatDau = lichhoc.thoiGianBatDau.TimeOfDay, // Lấy giờ, phút, giây của thời gian bắt đầu
                                   ThoiGianKetThuc = lichhoc.thoiGianKetThuc.TimeOfDay, // Lấy giờ, phút, giây của thời gian kết thúc
                                   PhongHoc = lichhoc.phonghoc
                               }).ToListAsync();

            var groupByNgayThoiGian = query.GroupBy(x => x.Date)
                                            .Select(g => new LichHoc_Select_All_v1
                                            {
                                                date = g.Key.ToString("yyyy-MM-dd"),
                                                lichmon = g.Select(item => new LichHoc_Select
                                                {
                                                    teacher = item.Teacher,
                                                    MonHoc = item.MonHoc,
                                                    id_LichHoc = item.IdLichHoc,
                                                    thoiGianBatDau = item.ThoiGianBatDau.ToString(),
                                                    thoiGianKetThuc = item.ThoiGianKetThuc.ToString(),
                                                    phonghoc = item.PhongHoc
                                                }).ToList()
                                            })
                                            .ToList();
            return groupByNgayThoiGian;
        }
    }
}
