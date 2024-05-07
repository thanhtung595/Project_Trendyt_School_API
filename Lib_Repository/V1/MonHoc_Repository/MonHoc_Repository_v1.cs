using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.LichHoc;
using Lib_Models.Models_Select.MonHoc;
using Lib_Models.Models_Select.Student;
using Lib_Models.Models_Select.Teacher;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Repository.V1.MonHoc_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.MonHoc
{
    public class MonHoc_Repository_v1 : IMonHoc_Repository_v1
    {
        private readonly Trendyt_DbContext _db;
        public MonHoc_Repository_v1(Trendyt_DbContext db)
        {
            _db = db;
        }

        public async Task<List<MonHoc_SelectAll_v1>> GetAll(tbMenberSchool menberManager)
        {
            IQueryable<tbMonHoc> queryidSchool = _db.tbMonHoc;
            queryidSchool = queryidSchool.Where(x => x.id_School == menberManager.id_School);
            if (menberManager.tbRoleSchool!.name_Role == "teacher" || menberManager.tbRoleSchool.name_Role == "student")
            {
                return await StudentTeacher(menberManager, queryidSchool);
            }
            else
            {
                return await AdminGet(menberManager, queryidSchool);
            }

            
        }

        public async Task<MonHocSelectById_v1> GetById(int idSchool,int id_MonHoc)
        {
            var list = await (from mh in _db.tbMonHoc
                              where mh.id_School == idSchool && mh.id_MonHoc == id_MonHoc
                              select new MonHocSelectById_v1
                              {
                                  id_MonHoc = mh.id_MonHoc,
                                  name_MonHoc = mh.name_MonHoc,
                                  danhGiaTrungBinh = mh._danhGiaTrungBinh,
                                  tag = mh.tags,
                                  soBuoiNghi = mh._SoBuoiNghi,
                                  ngayBatDau = mh.ngayBatDau,
                                  ngayKetThuc = mh.ngayKetThuc,
                                  giangvien = (from st in _db.tbMonHocClass_Student
                                               where st.id_MonHoc == mh.id_MonHoc
                                               join m in _db.tbMenberSchool
                                               on st.id_MenberSchool equals m.id_MenberSchool
                                               join r in _db.tbRoleSchool
                                               on m.id_RoleSchool equals r.id_RoleSchool
                                               where r.name_Role == "teacher"
                                               join ac in _db.tbAccount
                                               on m.id_Account equals ac.id_Account
                                               join k in _db.tbKhoaSchool
                                               on m.id_KhoaSchool equals k.id_KhoaSchool
                                               select new Select_One_Teacher_v1
                                               {
                                                   id_Teacher = m.id_MenberSchool,
                                                   user_Name = ac.user_Name,
                                                   fullName = ac.fullName,
                                                   name_Khoa = k.name_Khoa,
                                                   ma_Khoa = k.ma_Khoa,
                                                   image_User = ac.image_User,
                                                   tag = m.tags
                                               }).FirstOrDefault(),

                                  coutnStudent = (from st in _db.tbMonHocClass_Student
                                                  where st.id_MonHoc == mh.id_MonHoc
                                                  join m in _db.tbMenberSchool
                                                  on st.id_MenberSchool equals m.id_MenberSchool
                                                  join r in _db.tbRoleSchool
                                                  on m.id_RoleSchool equals r.id_RoleSchool
                                                  where r.name_Role == "student"
                                                  select st).Count(),

                                  student = (from st in _db.tbMonHocClass_Student
                                             where st.id_MonHoc == mh.id_MonHoc
                                             join m in _db.tbMenberSchool
                                             on st.id_MenberSchool equals m.id_MenberSchool
                                             join r in _db.tbRoleSchool
                                             on m.id_RoleSchool equals r.id_RoleSchool
                                             where r.name_Role == "student"
                                             join ac in _db.tbAccount
                                             on m.id_Account equals ac.id_Account
                                             join k in _db.tbKhoaSchool
                                             on m.id_KhoaSchool equals k.id_KhoaSchool
                                             select new Student_Select_v1
                                             {
                                                 id_Student = m.id_MenberSchool,
                                                 user_Name = ac.user_Name,
                                                 fullName = ac.fullName,
                                                 name_Khoa = k.name_Khoa,
                                                 ma_Khoa = k.ma_Khoa,
                                                 image_User = ac.image_User,
                                                 sex_User = ac.sex_User,
                                                 email_User = ac.email_User,
                                                 phone_User = ac.phone_User
                                             }).ToList()
                              }).FirstOrDefaultAsync();
            return list!;
        }

        public async Task<Status_Application> Insert(tbMonHoc monHoc)
        {
            try
            {
                await _db.tbMonHoc.AddAsync(monHoc);
                await _db.SaveChangesAsync();
                return new Status_Application { StatusBool = true ,StatusType = "success",Id_Int = monHoc.id_MonHoc};
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false , StatusType = "error"+ex.Message };
            }
        }

        private async Task<List<MonHoc_SelectAll_v1>> AdminGet(tbMenberSchool menberManager, IQueryable<tbMonHoc> queryidSchool)
        {
            var list = await (from mh in queryidSchool
                              select new MonHoc_SelectAll_v1
                              {
                                  id_MonHoc = mh.id_MonHoc,
                                  name_MonHoc = mh.name_MonHoc,
                                  danhGiaTrungBinh = mh._danhGiaTrungBinh,
                                  tag = mh.tags,
                                  soBuoiNghi = mh._SoBuoiNghi,
                                  soBuoiHoc = mh._SoBuoiHoc,
                                  ngayBatDau = mh.ngayBatDau,
                                  ngayKetThuc = mh.ngayKetThuc,
                                  giangvien = (from st in _db.tbMonHocClass_Student
                                               where st.id_MonHoc == mh.id_MonHoc
                                               join m in _db.tbMenberSchool
                                               on st.id_MenberSchool equals m.id_MenberSchool
                                               join r in _db.tbRoleSchool
                                               on m.id_RoleSchool equals r.id_RoleSchool
                                               where r.name_Role == "teacher"
                                               join ac in _db.tbAccount
                                               on m.id_Account equals ac.id_Account
                                               join k in _db.tbKhoaSchool
                                               on m.id_KhoaSchool equals k.id_KhoaSchool
                                               select new Select_One_Teacher_v1
                                               {
                                                   id_Teacher = m.id_MenberSchool,
                                                   user_Name = ac.user_Name,
                                                   fullName = ac.fullName,
                                                   name_Khoa = k.name_Khoa,
                                                   ma_Khoa = k.ma_Khoa,
                                                   image_User = ac.image_User,
                                                   tag = m.tags
                                               }).FirstOrDefault(),

                                  coutnStudent = (from st in _db.tbMonHocClass_Student
                                                  where st.id_MonHoc == mh.id_MonHoc
                                                  join m in _db.tbMenberSchool
                                                  on st.id_MenberSchool equals m.id_MenberSchool
                                                  join r in _db.tbRoleSchool
                                                  on m.id_RoleSchool equals r.id_RoleSchool
                                                  where r.name_Role == "student"
                                                  select st).Count(),

                                  student = (from st in _db.tbMonHocClass_Student
                                             where st.id_MonHoc == mh.id_MonHoc
                                             join m in _db.tbMenberSchool
                                             on st.id_MenberSchool equals m.id_MenberSchool
                                             join r in _db.tbRoleSchool
                                             on m.id_RoleSchool equals r.id_RoleSchool
                                             where r.name_Role == "student"
                                             join ac in _db.tbAccount
                                             on m.id_Account equals ac.id_Account
                                             join k in _db.tbKhoaSchool
                                             on m.id_KhoaSchool equals k.id_KhoaSchool
                                             select new Student_Select_v1
                                             {
                                                 id_Student = m.id_MenberSchool,
                                                 user_Name = ac.user_Name,
                                                 fullName = ac.fullName,
                                                 name_Khoa = k.name_Khoa,
                                                 ma_Khoa = k.ma_Khoa,
                                                 image_User = ac.image_User,
                                                 email_User = ac.email_User,
                                                 phone_User = ac.phone_User,
                                                 sex_User = ac.sex_User
                                             }).ToList(),
                                  lichhoc = (from lh in _db.tbLichHoc
                                             where lh.id_MonHoc == mh.id_MonHoc
                                             select new LichHoc_MonHoc_Select_v1
                                             {
                                                 id_LichHoc = lh.id_LichHoc,
                                                 thoiGianBatDau = lh.thoiGianBatDau,
                                                 thoiGianKetThuc = lh.thoiGianKetThuc
                                             }).ToList()
                              }).ToListAsync();
            return list;
        }

        private async Task<List<MonHoc_SelectAll_v1>> StudentTeacher(tbMenberSchool menberManager, IQueryable<tbMonHoc> queryidSchool)
        {
            var list = await (from st in _db.tbMonHocClass_Student
                              join mh in queryidSchool
                              on st.id_MonHoc equals mh.id_MonHoc
                              where st.id_MenberSchool == menberManager.id_MenberSchool
                              select new MonHoc_SelectAll_v1
                              {
                                  id_MonHoc = mh.id_MonHoc,
                                  name_MonHoc = mh.name_MonHoc,
                                  danhGiaTrungBinh = mh._danhGiaTrungBinh,
                                  tag = mh.tags,
                                  soBuoiNghi = mh._SoBuoiNghi,
                                  soBuoiHoc = mh._SoBuoiHoc,
                                  ngayBatDau = mh.ngayBatDau,
                                  ngayKetThuc = mh.ngayKetThuc,
                                  giangvien = (from st in _db.tbMonHocClass_Student
                                               where st.id_MonHoc == mh.id_MonHoc
                                               join m in _db.tbMenberSchool
                                               on st.id_MenberSchool equals m.id_MenberSchool
                                               join r in _db.tbRoleSchool
                                               on m.id_RoleSchool equals r.id_RoleSchool
                                               where r.name_Role == "teacher"
                                               join ac in _db.tbAccount
                                               on m.id_Account equals ac.id_Account
                                               join k in _db.tbKhoaSchool
                                               on m.id_KhoaSchool equals k.id_KhoaSchool
                                               select new Select_One_Teacher_v1
                                               {
                                                   id_Teacher = m.id_MenberSchool,
                                                   user_Name = ac.user_Name,
                                                   fullName = ac.fullName,
                                                   name_Khoa = k.name_Khoa,
                                                   ma_Khoa = k.ma_Khoa,
                                                   image_User = ac.image_User,
                                                   tag = m.tags
                                               }).FirstOrDefault(),

                                  coutnStudent = (from st in _db.tbMonHocClass_Student
                                                  where st.id_MonHoc == mh.id_MonHoc
                                                  join m in _db.tbMenberSchool
                                                  on st.id_MenberSchool equals m.id_MenberSchool
                                                  join r in _db.tbRoleSchool
                                                  on m.id_RoleSchool equals r.id_RoleSchool
                                                  where r.name_Role == "student"
                                                  select st).Count(),

                                  student = (from st in _db.tbMonHocClass_Student
                                             where st.id_MonHoc == mh.id_MonHoc
                                             join m in _db.tbMenberSchool
                                             on st.id_MenberSchool equals m.id_MenberSchool
                                             join r in _db.tbRoleSchool
                                             on m.id_RoleSchool equals r.id_RoleSchool
                                             where r.name_Role == "student"
                                             join ac in _db.tbAccount
                                             on m.id_Account equals ac.id_Account
                                             join k in _db.tbKhoaSchool
                                             on m.id_KhoaSchool equals k.id_KhoaSchool
                                             select new Student_Select_v1
                                             {
                                                 id_Student = m.id_MenberSchool,
                                                 user_Name = ac.user_Name,
                                                 fullName = ac.fullName,
                                                 name_Khoa = k.name_Khoa,
                                                 ma_Khoa = k.ma_Khoa,
                                                 image_User = ac.image_User,
                                                 email_User = ac.email_User,
                                                 phone_User = ac.phone_User,
                                                 sex_User = ac.sex_User
                                             }).ToList(),
                              }).ToListAsync();
            return list;
        }
    }
}
