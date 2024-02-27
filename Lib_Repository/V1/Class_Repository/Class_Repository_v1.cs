using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.Class;
using Lib_Models.Status_Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Class_Repository
{
    public class Class_Repository_v1 : IClass_Repository_v1
    {
        private readonly Trendyt_DbContext _db;
        public Class_Repository_v1(Trendyt_DbContext db)
        {
            _db = db;
        }

        #region SelectAll
        public async Task<List<Class_Select_v1>> SelectAll(tbMenberSchool menberSchoolManager)
        {

            if (menberSchoolManager.id_KhoaSchool == 0 && menberSchoolManager.tbRoleSchool!.name_Role == "school management")
            {
                return await SchoolManagementSelectAll(menberSchoolManager);
            }
            else if (menberSchoolManager.id_KhoaSchool != 0)
            {
                return await KhoaManagementSelectAll(menberSchoolManager);
            }
            return null!;
        }
        #endregion
        
        #region SelectById
        public Task<Class_Select_v1> SelectById()
        {
            throw new NotImplementedException();
        }
        #endregion
        
        #region InsertAsync
        public async Task<Status_Application> InsertAsync(tbClassSchool classSchool)
        {
            try
            {
                await _db.tbClassSchool.AddAsync(classSchool);
                await _db.SaveChangesAsync();
                return new Status_Application { StatusBool = true, StatusType = "success" };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false, StatusType = "error: " + ex.Message };
            }
        }
        #endregion

        #region UpdateAsync
        public Task<Status_Application> UpdateAsync(tbClassSchool classSchool)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region School Management Select All
        public async Task<List<Class_Select_v1>> SchoolManagementSelectAll(tbMenberSchool menberSchoolManager)
        {

            var list = await (from cl in _db.tbClassSchool
                              join k in _db.tbKhoaSchool
                              on cl.id_KhoaSchool equals k.id_KhoaSchool
                              join s in _db.tbSchool
                              on k.id_School equals s.id_School
                              where s.id_School == menberSchoolManager.id_School
                              select new Class_Select_v1
                              {
                                  id_ClassSchool = cl.id_ClassSchool,
                                  name_ClassSchool = cl.name_ClassSchool,
                                  ma_Khoa = k.ma_Khoa,
                                  name_Khoa = k.name_Khoa,
                                  tags = cl.tags
                              }).ToListAsync();
            return list;
        }
        #endregion

        #region Khoa Management Select All
        public async Task<List<Class_Select_v1>> KhoaManagementSelectAll(tbMenberSchool menberSchoolManager)
        {
            var list = await (from cl in _db.tbClassSchool
                              join k in _db.tbKhoaSchool
                              on cl.id_KhoaSchool equals k.id_KhoaSchool
                              where k.id_KhoaSchool == menberSchoolManager.id_KhoaSchool
                              join s in _db.tbSchool
                              on k.id_School equals s.id_School
                              where s.id_School == menberSchoolManager.id_School
                              select new Class_Select_v1
                              {
                                  id_ClassSchool = cl.id_ClassSchool,
                                  name_ClassSchool = cl.name_ClassSchool,
                                  ma_Khoa = k.ma_Khoa,
                                  name_Khoa = k.name_Khoa,
                                  tags = cl.tags
                              }).ToListAsync();
            return list;
        }
        #endregion
    }
}
