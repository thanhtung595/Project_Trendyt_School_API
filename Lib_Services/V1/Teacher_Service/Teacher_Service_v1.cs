using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Models_Select.Teacher;
using Lib_Repository.V1.Teacher_Repository;
using Lib_Services.Token_Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Teacher_Service
{
    public class Teacher_Service_v1 : ITeacher_Service_v1
    {
        private readonly ITeacher_Repository_v1 _teacher_Repository_V1;
        private readonly IToken_Service_v1 _token_Service_v1;
        private readonly Trendyt_DbContext _db;
        public Teacher_Service_v1(ITeacher_Repository_v1 teacher_Repository_V1 , IToken_Service_v1 token_Service_V1,
            Trendyt_DbContext db)
        {
            _teacher_Repository_V1 = teacher_Repository_V1;
            _token_Service_v1 = token_Service_V1;
            _db = db;
        }

        #region Select_All_Teacher
        public async Task<List<Select_All_Teacher_v1>> Select_All_Teacher()
        {
            int id_account = await _token_Service_v1.GetAccessTokenIdAccount();
            var id_MenberManager = await _db.tbMenberSchool.FirstOrDefaultAsync(x => x.id_Account == id_account);

            return await _teacher_Repository_V1.Select_All_Teacher(id_MenberManager!.id_School);
        }
        #endregion
    }
}
