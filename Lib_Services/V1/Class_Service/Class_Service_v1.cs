using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.Class;
using Lib_Models.Status_Model;
using Lib_Repository.V1.Class_Repository;
using Lib_Services.Token_Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Class_Service
{
    public class Class_Service_v1 : IClass_Service_v1
    {
        private readonly Trendyt_DbContext _db;
        private readonly IClass_Repository_v1 _class_Repository_V1;
        private readonly IToken_Service_v1 _token_Service_V1;
        public Class_Service_v1(Trendyt_DbContext db , IClass_Repository_v1 class_Repository_V1,
            IToken_Service_v1 token_Service_V1)
        {
            _db = db;
            _class_Repository_V1 = class_Repository_V1;
            _token_Service_V1 = token_Service_V1;
        }

        #region SelectAll
        public async Task<List<Class_Select_v1>> SelectAll()
        {
            // Id_Menber Manager
            int id_MenberManager = await _token_Service_V1.GetAccessTokenIdAccount();
            //var menberManager = await _db.tbMenberSchool.FirstOrDefaultAsync(x => x.id_Account == id_MenberManager);
            var menberManager = await _db.tbMenberSchool
                                .Include(menber => menber.tbRoleSchool)
                                .FirstOrDefaultAsync(x => x.id_Account == id_MenberManager);

            return await _class_Repository_V1.SelectAll(menberManager!);
        }
        #endregion

        #region SelectById
        public Task<Class_Select_v1> SelectById()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region InsertAsync
        public async Task<Status_Application> InsertAsync(string name_ClassSchool)
        {
            name_ClassSchool = name_ClassSchool.Trim();
            // Id_Menber Manager
            int id_MenberManager = await _token_Service_V1.GetAccessTokenIdAccount();
            //var menberManager = await _db.tbMenberSchool.FirstOrDefaultAsync(x => x.id_Account == id_MenberManager);
            var menberManager = await _db.tbMenberSchool.FirstOrDefaultAsync(x => x.id_Account == id_MenberManager);
            var khoa = await _db.tbKhoaSchool.FindAsync(menberManager!.id_KhoaSchool);
            if (khoa == null)
            {
                return new Status_Application { StatusBool = false, StatusType ="Tài khoản chưa gia nhập khoa"};
            }

            var isNameClass = await _db.tbClassSchool.AnyAsync(x => x.id_KhoaSchool == khoa.id_KhoaSchool && x.name_ClassSchool!.ToLower() == name_ClassSchool.ToLower());
            if (isNameClass)
            {
                return new Status_Application { StatusBool = false, StatusType = "Tên lớp đã tồn tại" };
            }

            tbClassSchool classSchool = new tbClassSchool
            {
                name_ClassSchool = name_ClassSchool,
                id_KhoaSchool = khoa.id_KhoaSchool,
                tags = "active"
            };
            return await _class_Repository_V1.InsertAsync(classSchool);
        }
        #endregion

        #region UpdateAsync
        public Task<Status_Application> UpdateAsync(string name_ClassSchool)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
