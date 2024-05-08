using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Class_Member_Repository
{
    public class Class_Member_Repository_v1 : IClass_Member_Repository_v1
    {
        //private readonly Trendyt_DbContext _db;
        //public Class_Member_Repository_v1(Trendyt_DbContext db)
        //{
        //    _db = db;
        //}

        //public async Task<Status_Application> Delete(tbClassSchool_Menber classMember)
        //{
        //    try
        //    {
        //        _db.tbClassSchool_Menber.Remove(classMember);
        //        await _db.SaveChangesAsync();
        //        return new Status_Application { StatusBool = true, StatusType = "success" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Status_Application { StatusBool = false, StatusType = "error" + ex.Message };
        //    }
        //}

        //public async Task<Status_Application> Insert(tbClassSchool_Menber classMember)
        //{
        //    try
        //    {
        //        await _db.tbClassSchool_Menber.AddAsync(classMember);
        //        await _db.SaveChangesAsync();
        //        return new Status_Application { StatusBool = true, StatusType = "success" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Status_Application { StatusBool = false, StatusType = "error"+ex.Message };
        //    }
        //}
        public Task<Status_Application> Delete(tbClassSchool_Menber classMember)
        {
            throw new NotImplementedException();
        }

        public Task<Status_Application> Insert(tbClassSchool_Menber classMember)
        {
            throw new NotImplementedException();
        }
    }
}
