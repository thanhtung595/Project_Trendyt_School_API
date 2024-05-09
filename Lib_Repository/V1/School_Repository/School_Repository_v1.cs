using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Update.School;
using Lib_Models.Models_Select.School;
using Lib_Models.Status_Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.School_Repository
{
    public class School_Repository_v1 : ISchool_Repository_v1
    {
        private readonly Trendyt_DbContext _db;
        public School_Repository_v1(Trendyt_DbContext db)
        {
            _db = db;
        }

        #region SelectAllAsync
        public async Task<List<School_SelectAll_v1>> SelectAllAsync()
        {
            try
            {
                var school = await (from sc in _db.tbSchool
                                    join ac in _db.tbAccount
                                    on sc.id_Account equals ac.id_Account
                                    select new School_SelectAll_v1
                                    {
                                        owner = ac.fullName,
                                        name_School = sc.name_School,
                                        description_School = sc.description_School,
                                        adderss_School = sc.adderss_School,
                                        evaluate_School = sc.evaluate_School
                                    }).ToListAsync();
                return school;
            }
            catch (Exception)
            {
                return null!;
            }
        }
        #endregion

        #region InsertAsync
        public async Task<Status_Application> InsertAsync(tbSchool school)
        {
            try
            {
                await _db.tbSchool.AddAsync(school);
                await _db.SaveChangesAsync();
                return new Status_Application { StatusBool = true, StatusType = "success", Id_Int = school.id_School };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false, StatusType = "error: " + ex };
            }
        }
        #endregion

        #region UpdateAsync
        public async Task<Status_Application> UpdateAsync(School_Update_v1 request)
        {
            try
            {
                var school = await _db.tbSchool.FirstOrDefaultAsync(x => x.id_Account == request.id_AccountToken);
                school!.name_School = request.name_School;
                school.adderss_School = request.adderss_School;
                school.description_School = request.description_School;

                await _db.SaveChangesAsync();
                return new Status_Application { StatusBool = true, StatusType = "success" };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false, StatusType = "error: " + ex };
            }
        }
        #endregion
    }
}
