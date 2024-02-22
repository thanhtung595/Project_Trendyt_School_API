using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Update.School;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.School;
using Lib_Models.Status_Model;
using Lib_Repository.V1.School_Repository;
using Lib_Services.Token_Service;
using Lib_Services.V1.Menber_Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.School_Service
{
    public class School_Service_v1 : ISchool_Service_v1
    {
        private readonly ISchool_Repository_v1 _school_Repository_v1;
        private readonly Trendyt_DbContext _db;
        private readonly IToken_Service_v1 _token_Service_v1;
        private readonly IMenber_Service_v1 _menber_Service_V1;
        public School_Service_v1(Trendyt_DbContext db, ISchool_Repository_v1 school_Repository_v1,
            IToken_Service_v1 token_Service_V1 , IMenber_Service_v1 menber_Service_V1)
        {
            _db = db;
            _school_Repository_v1 = school_Repository_v1;
            _token_Service_v1 = token_Service_V1;
            _menber_Service_V1 = menber_Service_V1;
        }

        #region SelectAllAsync
        public async Task<List<School_SelectAll_v1>> SelectAllAsync()
        {
            return await _school_Repository_v1.SelectAllAsync();
        }
        #endregion

        #region InsertAsync
        public async Task<Status_Application> InsertAsync(School_Insert_v1 request)
        {
            // Kiểm tra request có null
            if (string.IsNullOrEmpty(request.owner))
            {
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập người sở hữu." };
            }
            if (string.IsNullOrEmpty(request.name_School))
            {
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập tên trường." };
            }
            if (string.IsNullOrEmpty(request.description_School))
            {
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập giới thiệu." };
            }
            if (string.IsNullOrEmpty(request.adderss_School))
            {
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập địa chỉ." };
            }
            // kiểm tra tài khoản người sở hữu
            var account = await _db.tbAccount.FirstOrDefaultAsync(x =>
                x.user_Name == request.owner);
            if (account == null)
            {
                return new Status_Application { StatusBool = false, StatusType = "Người sở hữu không tồn tại." };
            }
            //Kiểm tra tài khoản này đã sở hữu school nào chưa
            bool is_School = await _db.tbSchool.AnyAsync(x => x.id_Account == account.id_Account);
            if (is_School)
            {
                return new Status_Application { StatusBool = false, StatusType = "Người sở hữu này đang sở hữu một school khác." };
            }
            // Kiểm tra tên trường có tồn tại.
            bool isName_school = await _db.tbSchool.AnyAsync(x => x.name_School!.ToLower() == request.name_School!.ToLower());
            if (isName_school)
            {
                return new Status_Application { StatusBool = false, StatusType = "Tên trường đã tồn tại." };
            }

            tbSchool school = new tbSchool
            {
                id_Account = account.id_Account,
                name_School = request.name_School!.Trim(),
                description_School = request.description_School!.Trim(),
                adderss_School = request.adderss_School!.Trim(),
                evaluate_School = 5
            };
            Status_Application status_AddSchool = await _school_Repository_v1.InsertAsync(school);
            if (status_AddSchool.StatusBool)
            {
                MenberSchool_Insert_v1 school_Insert_Modele = new MenberSchool_Insert_v1
                {
                    id_Account = account.id_Account,
                    name_Role = "school management",
                    id_School  = status_AddSchool.Id_Int
                };
                Status_Application status_AddMenber = await _menber_Service_V1.InsertAsync(school_Insert_Modele);
                if (status_AddMenber.StatusBool)
                {
                    return status_AddSchool;
                }
            }
            return new Status_Application { StatusBool = false, StatusType = "error server" };
        }
        #endregion

        #region UpdateAsync
        public async Task<Status_Application> UpdateAsync(School_Update_v1 request)
        {
            if (string.IsNullOrEmpty(request.name_School))
            {
                request.name_School!.Trim();
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập tên trường." };
            }
            if (string.IsNullOrEmpty(request.description_School))
            {
                request.description_School!.Trim();
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập giới thiệu." };
            }
            if (string.IsNullOrEmpty(request.adderss_School))
            {
                request.adderss_School!.Trim();
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập địa chỉ." };
            }

            int id_AccountToken = await _token_Service_v1.GetAccessTokenIdAccount();
            request.id_AccountToken = id_AccountToken;

            var menber = await _db.tbMenberSchool.FirstOrDefaultAsync(x => x.id_Account ==  id_AccountToken);
            // Check name school có bị trùng
            var school = await _db.tbSchool.FirstOrDefaultAsync(x => x.id_School != menber!.id_School && x.name_School!.ToLower() == request.name_School.ToLower());
            if (school != null)
            {
                return new Status_Application { StatusBool = false, StatusType = "Bị trùng tên trường với trường khác." }; 
            }
            return await _school_Repository_v1.UpdateAsync(request);
        }
        #endregion
    }
}
