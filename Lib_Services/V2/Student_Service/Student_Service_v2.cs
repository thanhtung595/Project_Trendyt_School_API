using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.Student;
using Lib_Models.Models_Select.Teacher;
using Lib_Repository.Abstract;
using Lib_Services.Token_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.Student_Service
{
    public class Student_Service_v2 : IStudent_Service_v2
    {
        private readonly IToken_Service_v2 _token_Service_v2;
        private readonly Trendyt_DbContext _db;
        private readonly IRepository<tbMenberSchool> _repositoryMenberSchool;
        private readonly IRepository<tbClassSchool_Menber> _repositoryClassSchool_Menber;
        public Student_Service_v2(IToken_Service_v2 token_Service_v2, Trendyt_DbContext db, IRepository<tbMenberSchool> repositoryMenberSchool,
            IRepository<tbClassSchool_Menber> repositoryClassSchool_Menber)
        {
            _token_Service_v2 = token_Service_v2;
            _db = db;
            _repositoryMenberSchool = repositoryMenberSchool;
            _repositoryClassSchool_Menber = repositoryClassSchool_Menber;

        }
        public async Task<List<Student_Select_v1>> SelectAllNotJoinClassAsync()
        {
            var menberManager = await _token_Service_v2.Get_Menber_Token();

            var query = await _repositoryMenberSchool.GetAllIncluding(r => r.tbRoleSchool!.name_Role == "student", a => a.tbAccount!);

            if (menberManager.id_KhoaSchool != 0)
            {
                query = query.Where(k => k.id_KhoaSchool == menberManager.id_KhoaSchool);
            }

            var memberClass = await _repositoryClassSchool_Menber.GetAllIncluding(m => m.tbMenberSchool!.tbRoleSchool!.name_Role == "student");

            // Chuyển đổi thành ToHashSet để tối ưu hiệu suất truy suất
            var memberClassIds = memberClass.Select(mc => mc.id_MenberSchool).ToHashSet();

            // Loại bỏ các Student đã tham gia lớp học
            var result = query.Where(m => !memberClassIds.Contains(m.id_MenberSchool))
                .Select(m => new Student_Select_v1
                {
                    fullName = m.tbAccount!.fullName,
                    user_Name = m.tbAccount.user_Name,
                    image_User = m.tbAccount!.image_User,
                    id_Student = m.id_MenberSchool,
                    sex_User = m.tbAccount.sex_User,
                    email_User = m.tbAccount.email_User,
                }).ToList();

            if (result == null)
            {
                return null!;
            }

            return result.ToList();
        }
    }
}
