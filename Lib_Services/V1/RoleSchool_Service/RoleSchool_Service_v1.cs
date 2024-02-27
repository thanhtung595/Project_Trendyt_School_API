using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using AutoMapper;
using Lib_Models.Model_Insert.v1;
using Lib_Models.Model_Update.Role;
using Lib_Models.Model_Update.RoleSchool;
using Lib_Models.Models_Select.Role;
using Lib_Models.Status_Model;
using Lib_Repository.V1.Role_Repository;
using Lib_Repository.V1.RoleSchool_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.RoleSchool_Service
{
    public class RoleSchool_Service_v1 : IRoleSchool_Service_v1
    {
        private readonly IRoleSchool_Repository_v1 _roleSchool_Repository_V1;
        private readonly Trendyt_DbContext _db;
        private readonly IMapper _mapper;

        public RoleSchool_Service_v1(Trendyt_DbContext db, IRoleSchool_Repository_v1 roleSchool_Repository_V1,
            IMapper mapper)
        {
            _db = db;
            _roleSchool_Repository_V1 = roleSchool_Repository_V1;
            _mapper = mapper;
        }
        public async Task<Status_Application> InsertAsync(Role_Insert_v1 request)
        {
            if (string.IsNullOrEmpty(request.name_Role))
            {
                return new Status_Application
                {
                    StatusBool = false,
                    StatusType = "Chưa nhập name role."
                };
            }

            //Check role tồn tại
            var roleSchool = await _db.tbRoleSchool.FirstOrDefaultAsync(x => x.name_Role!.ToLower() == request.name_Role.ToLower());
            if (roleSchool != null)
            {
                return new Status_Application
                {
                    StatusBool = false,
                    StatusType = "Name role đã tồn tại."
                };
            }

            //Add role school
            tbRoleSchool tbRoleSchool = new tbRoleSchool
            {
                id_RoleSchool = Guid.NewGuid(),
                name_Role = request.name_Role,
            };

            return await _roleSchool_Repository_V1.InsertAsync(tbRoleSchool);
        }

        public async Task<List<Role_Select_v1>> SelectAllAsync()
        {
            return _mapper.Map<List<Role_Select_v1>>(await _roleSchool_Repository_V1.SelectAllAsync());
        }

        public async Task<Status_Application> Update_Role_Account(UpdateRoleSchool request)
        {
            try
            {
                var account = await _db.tbAccount.FirstOrDefaultAsync(x =>
                x.user_Name == request.user_Name);

                if (account == null)
                {
                    return new Status_Application { StatusBool = false, StatusType = "Tài khoản không tồn tại." };
                }

                var menber = await _db.tbMenberSchool.FirstOrDefaultAsync(x =>
                    x.id_Account == account.id_Account);

                if (menber == null)
                {
                    return new Status_Application { StatusBool = false, StatusType = "Tài khoản không tồn tại trong school." };
                }

                var roleSchhol = await _db.tbRoleSchool.FirstOrDefaultAsync(x =>
                    x.name_Role == request.name_Role);

                if (roleSchhol == null)
                {
                    return new Status_Application { StatusBool = false, StatusType = "Role không tồn tại." };
                }

                menber.id_RoleSchool = roleSchhol.id_RoleSchool;

                await _db.SaveChangesAsync();

                return new Status_Application { StatusBool = true, StatusType = "Success." };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false, StatusType = "Error: " + ex.Message };
            }
        }

        public async Task<Status_Application> Update_Role_Menber(UpdateRoleSchool request)
        {
            // Check null value
            if (string.IsNullOrEmpty(request.name_Role))
            {
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập name role" };
            }
            return await _roleSchool_Repository_V1.Update_Role_Menber(request);
        }
    }
}
