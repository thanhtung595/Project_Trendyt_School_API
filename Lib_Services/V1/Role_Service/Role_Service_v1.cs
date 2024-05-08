using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using AutoMapper;
using Lib_Models.Model_Insert.v1;
using Lib_Models.Model_Update.Role;
using Lib_Models.Models_Select.Role;
using Lib_Models.Status_Model;
using Lib_Repository.V1.Role_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Role_Service
{
    public class Role_Service_v1 : IRole_Service_v1
    {
        //private readonly Trendyt_DbContext _db;
        //private readonly IRole_Repository_v1 _role_Repository_V1;
        //private readonly IMapper _mapper;

        //public Role_Service_v1(Trendyt_DbContext db, IRole_Repository_v1 role_Repository_V1,
        //    IMapper mapper)
        //{
        //    _db = db;
        //    _role_Repository_V1 = role_Repository_V1;
        //    _mapper = mapper;
        //}

        //public async Task<List<Role_Select_v1>> SelectAllAsync()
        //{
        //    return _mapper.Map<List<Role_Select_v1>>(await _role_Repository_V1.SelectAllAsync());
        //}

        //public async Task<Status_Application> InsertAsync(Role_Insert_v1 request)
        //{
        //    if (string.IsNullOrEmpty(request.name_Role)) 
        //    {
        //        return new Status_Application
        //        {
        //            StatusBool = false, StatusType = "Chưa nhập name role."
        //        };
        //    }

        //    //Check role tồn tại
        //    var role = await _db.tbRole.FirstOrDefaultAsync(x => x.name_Role!.ToLower() == request.name_Role.ToLower());
        //    if (role != null)
        //    {
        //        return new Status_Application
        //        {
        //            StatusBool = false,
        //            StatusType = "Name role đã tồn tại."
        //        };
        //    }

        //    //Add role
        //    tbRole tbRole = new tbRole 
        //    {
        //        id_Role = Guid.NewGuid(),
        //        name_Role = request.name_Role,
        //    };

        //    return await _role_Repository_V1.InsertAsync(tbRole);
        //}

        //public async Task<Status_Application> Update_Role_Account(UpdateRoleAccount request)
        //{
        //    try
        //    {
        //        var account = await _db.tbAccount.FirstOrDefaultAsync(x =>
        //        x.user_Name == request.user_Name);

        //        if (account == null)
        //        {
        //            return new Status_Application { StatusBool = false, StatusType = "Tài khoản không tồn tại." };
        //        }

        //        var role = await _db.tbRole.FirstOrDefaultAsync(x =>
        //            x.name_Role == request.name_Role);

        //        if (role == null)
        //        {
        //            return new Status_Application { StatusBool = false, StatusType = "Role không tồn tại." };
        //        }

        //        account.id_Role = role.id_Role;

        //        await _db.SaveChangesAsync();

        //        return new Status_Application { StatusBool = true, StatusType = "Success." };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Status_Application { StatusBool = false, StatusType = "Error: "+ex };
        //    }
        //}
        public Task<Status_Application> InsertAsync(Role_Insert_v1 request)
        {
            throw new NotImplementedException();
        }

        public Task<List<Role_Select_v1>> SelectAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Status_Application> Update_Role_Account(UpdateRoleAccount request)
        {
            throw new NotImplementedException();
        }
    }
}
