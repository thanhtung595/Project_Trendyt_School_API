using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Status_Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.TypeAccount_Repository
{
    public class TypeAccount_Repository_v1 : ITypeAccount_Repository_v1
    {
        //private readonly Trendyt_DbContext _db;
        //public TypeAccount_Repository_v1(Trendyt_DbContext db)
        //{
        //    _db = db;
        //}
        //public async Task<Status_Application> InsertAsync(tbTypeAccount typeAccount)
        //{
        //    try
        //    {
        //        await _db.tbTypeAccount.AddAsync(typeAccount);
        //        await _db.SaveChangesAsync();
        //        return new Status_Application
        //        {
        //            StatusBool = true,
        //            StatusType = "success"
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Status_Application
        //        {
        //            StatusBool = true,
        //            StatusType = "error: "+ex.Message
        //        };
        //    }
        //}

        //public async Task<List<tbTypeAccount>> SelectAll()
        //{
        //    return await _db.tbTypeAccount.ToListAsync();
        //}
        public Task<Status_Application> InsertAsync(tbTypeAccount typeAccount)
        {
            throw new NotImplementedException();
        }

        public Task<List<tbTypeAccount>> SelectAll()
        {
            throw new NotImplementedException();
        }
    }
}
