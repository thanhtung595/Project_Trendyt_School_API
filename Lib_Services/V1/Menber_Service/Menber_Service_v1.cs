using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Update.School;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.Menber;
using Lib_Models.Status_Model;
using Lib_Repository.V1.Menber_Repository;
using Lib_Services.Token_Service;
using Lib_Services.V1.Register_Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Menber_Service
{
    public class Menber_Service_v1 : IMenber_Service_v1
    {
        private readonly IMenber_Repository_v1 _menber_Repository_V1;
        private readonly IRegister_Service_v1 _register_Service_V1;
        private readonly Trendyt_DbContext _db;
        private readonly IToken_Service_v1 _token_Service_V1;
        private readonly IToken_Service_v2 _token_Service_V2;
        public Menber_Service_v1(Trendyt_DbContext db, IMenber_Repository_v1 menber_Repository_V1,
            IToken_Service_v1 token_Service_V1, IRegister_Service_v1 register_Service_V1, IToken_Service_v2 token_Service_V2)
        {
            _db = db;
            _menber_Repository_V1 = menber_Repository_V1;
            _token_Service_V1 = token_Service_V1;
            _register_Service_V1 = register_Service_V1;
            _token_Service_V2 = token_Service_V2;
        }

        #region Select All Menber Async
        public async Task<List<Menber_SclectAll_v1>> SelectAllAsync()
        {
            // Lấy id_school
            var menberManager = await _token_Service_V2.Get_Menber_Token();

            return await _menber_Repository_V1.SelectAllAsync(menberManager!);
        }
        #endregion

        #region Insert Menber Async
        public async Task<Status_Application> InsertAsync(MenberSchool_Insert_v1 request)
        {
            // Kiểm tra nếu id_Account == 0 thì là tạo mới nếu id_Account != 0 là account có sẵn
            if (request.id_Account == 0)
            {
                // Register account
                Register_Insert_v1 registerModel = new Register_Insert_v1
                {
                    user_Name = request.user_Name,
                    fullName = request.fullName,
                    user_Password = request.user_Password,
                    email_User = request.email_User
                };
                Status_Application register_Account = await _register_Service_V1.RegisterUserName(registerModel);
                if (!register_Account.StatusBool)
                {
                    return new Status_Application { StatusBool = false, StatusType = register_Account.StatusType };
                }
                // Lấy id Account
                request.id_Account = register_Account.Id_Int;

                // Lấy id_school
                var menberManager = await _token_Service_V2.Get_Menber_Token();
                request.id_School = menberManager!.id_School;
            }

            // Set role school
            if (string.IsNullOrEmpty(request.name_Role))
            {
                request.name_Role = "student";
            }
            var roleSchool = await _db.tbRoleSchool.FirstOrDefaultAsync(x => x.name_Role == request.name_Role);

            var tag = await _db.tbTag.FirstOrDefaultAsync(t => t.name == "active");
            // Add Menber
            tbMenberSchool menberSchool = new tbMenberSchool
            {
                id_Account = request.id_Account,
                id_KhoaSchool = request.id_KhoaSchool,
                id_RoleSchool = roleSchool!.id_RoleSchool,
                danhGiaTb = 5,
                id_Tag = tag!.id_Tag,
                id_School = request.id_School
            };
            return await _menber_Repository_V1.InsertAsync(menberSchool);
        }
        #endregion

        #region School Menber Update Async
        public async Task<Status_Application> SchoolMenberUpdateAsync(School_Menber_Update_v1 request)
        {
            return await _menber_Repository_V1.SchoolMenberUpdateAsync(request);
        }

        #endregion

        #region Profile
        public async Task<Member_Profile_v1> Profile()
        {
            int id_Account = await _token_Service_V2.Get_Id_Account_Token();
            return await _menber_Repository_V1.Profile(id_Account);
        }
        #endregion

        #region Delete
        public async Task<Status_Application> Delete(int id_member)
        {
            // Lấy id_school
            var menberManager = await _token_Service_V2.Get_Menber_Token();

            // Menber school

            var menber = await _db.tbMenberSchool.FindAsync(id_member);
            if (menber == null && menber!.id_School != menberManager!.id_School)
            {
                return new Status_Application { StatusBool = false, StatusType = "Member delete không hợp lệ" };
            }

            return await _menber_Repository_V1.Delete(menber);
        }
        #endregion
    }
}
