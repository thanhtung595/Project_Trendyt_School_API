using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Menber_Repository
{
    public class Menber_Repository_v1 : IMenber_Repository_v1
    {
        private readonly Trendyt_DbContext _db;
        public Menber_Repository_v1(Trendyt_DbContext db)
        {
            _db = db;
        }

        #region InsertAsync
        public async Task<Status_Application> InsertAsync(tbMenberSchool menberSchool)
        {
			try
			{
                await _db.tbMenberSchool.AddAsync(menberSchool);
                await _db.SaveChangesAsync();
                return new Status_Application
                {
                    StatusBool = true,
                    StatusType = "success"
                };
			}
			catch (Exception ex)
			{
                return new Status_Application { StatusBool = false , StatusType = $"error menber repository {ex}" };
			}
        }
        #endregion
    }
}
