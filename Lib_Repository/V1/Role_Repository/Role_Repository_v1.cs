using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Status_Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Role_Repository
{
    public class Role_Repository_v1 : IRole_Repository_v1
    {
        private readonly Trendyt_DbContext _db;

        public Role_Repository_v1(Trendyt_DbContext db)
        {
            _db = db;
        }
        public async Task<List<tbRole>> SelectAllAsync()
        {
            return await _db.tbRole.ToListAsync();
        }

        public async Task<Status_Application> InsertAsync(tbRole role)
        {
            try
            {
                await _db.tbRole.AddAsync(role);
                await _db.SaveChangesAsync();
                return new Status_Application
                {
                    StatusBool = true,
                    StatusType = "success"
                };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false, StatusType = "error: "+ex };
            }
        }
    }
}
