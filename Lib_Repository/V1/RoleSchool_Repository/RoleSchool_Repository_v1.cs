using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Update.RoleSchool;
using Lib_Models.Status_Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.RoleSchool_Repository
{
    public class RoleSchool_Repository_v1 : IRoleSchool_Repository_v1
    {
        //private readonly Trendyt_DbContext _db;
        //public RoleSchool_Repository_v1(Trendyt_DbContext db)
        //{
        //    _db = db;
        //}
        //public async Task<List<tbRoleSchool>> SelectAllAsync()
        //{
        //    return await _db.tbRoleSchool.ToListAsync();
        //}

        //public async Task<Status_Application> InsertAsync(tbRoleSchool role)
        //{
        //    try
        //    {
        //        await _db.tbRoleSchool.AddAsync(role);
        //        await _db.SaveChangesAsync();
        //        return new Status_Application
        //        {
        //            StatusBool = true,
        //            StatusType = "success"
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Status_Application { StatusBool = false, StatusType = "error: " + ex };
        //    }
        //}

        //public async Task<Status_Application> Update_Role_Menber(UpdateRoleSchool request)
        //{
        //    try
        //    {
        //        // Check isMenber
        //        tbMenberSchool? menberSchool = await _db.tbMenberSchool.FindAsync(request.id_MenberSchool);
        //        if (menberSchool!.id_MenberSchool == 0)
        //        {
        //            return new Status_Application { StatusBool = false, StatusType = "Không tìm thấy menber" };
        //        }
        //        // Check name role
        //        var roleSchool = await _db.tbRoleSchool.FirstOrDefaultAsync(x => x.name_Role!.ToLower() == request.name_Role!.ToLower());

        //        if (menberSchool!.id_MenberSchool == 0)
        //        {
        //            return new Status_Application { StatusBool = false, StatusType = "Không tìm thấy menber" };
        //        }
        //        return null!;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Status_Application { StatusBool = false, StatusType = "error " + ex };
        //    }
        //}
        public Task<Status_Application> InsertAsync(tbRoleSchool role)
        {
            throw new NotImplementedException();
        }

        public Task<List<tbRoleSchool>> SelectAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Status_Application> Update_Role_Menber(UpdateRoleSchool request)
        {
            throw new NotImplementedException();
        }
    }
}
