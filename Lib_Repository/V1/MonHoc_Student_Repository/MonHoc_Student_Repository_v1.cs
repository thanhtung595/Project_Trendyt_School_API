using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.MonHoc_Student_Repository
{
    public class MonHoc_Student_Repository_v1 : IMonHoc_Student_Repository_v1
    {
        //private readonly Trendyt_DbContext _db;
        //public MonHoc_Student_Repository_v1(Trendyt_DbContext db)
        //{
        //    _db = db;
        //}
        //public async Task<Status_Application> Insert(tbMonHocClass_Student student)
        //{
        //    try
        //    {
        //        await _db.tbMonHocClass_Student.AddAsync(student);
        //        await _db.SaveChangesAsync();
        //        return new Status_Application { StatusBool = true, StatusType = "success" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Status_Application { StatusBool = false ,StatusType = "error" +ex.Message };
        //    }
        //}
        public Task<Status_Application> Insert(tbMonHocClass_Student student)
        {
            throw new NotImplementedException();
        }
    }
}
