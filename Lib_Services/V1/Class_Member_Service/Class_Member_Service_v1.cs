using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Insert.v1.Class_School;
using Lib_Models.Status_Model;
using Lib_Repository.V1.Class_Member_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Class_Member_Service
{
    public class Class_Member_Service_v1 : IClass_Member_Service_v1
    {
        private readonly Trendyt_DbContext _db;
        private readonly IClass_Member_Repository_v1 _class_Member_Repository_V1;
        public Class_Member_Service_v1(Trendyt_DbContext db, IClass_Member_Repository_v1 class_Member_Repository_V1)
        {
            _db = db;
            _class_Member_Repository_V1 = class_Member_Repository_V1;
        }


        public async Task<Status_Application> Insert(Class_Member_Insert_v1 request)
        {
            // Kiểm tra lớp có tồn tại
            var isLop = await _db.tbClassSchool.FindAsync(request.id_ClassSchool);
            if (isLop == null)
            {
                return new Status_Application { StatusBool = false,StatusType = "Lớp không tồn tại" };
            }
            // Kiểm tra member có tồn tại
            var ismember = await _db.tbMenberSchool.FindAsync(request.id_Student);
            if (ismember == null)
            {
                return new Status_Application { StatusBool = false, StatusType = $"Member id {request.id_Student} không tồn tại" };
            }

            // Kiểm tra member đã tồn tại trong lớp
            var studentClass = await _db.tbClassSchool_Menber.AnyAsync(x => x.id_MenberSchool == ismember.id_MenberSchool);
            if (studentClass)
            {
                return new Status_Application { StatusBool = false, StatusType = $"Member id {request.id_Student} đã trong lớp" };
            }

            // Kiểm tra role
            var role = await _db.tbRoleSchool.FindAsync(ismember.id_RoleSchool);
            if (role!.name_Role != "student" && role!.name_Role != "teacher")
            {
                return new Status_Application { StatusBool = false, StatusType = $"Member id {request.id_Student} không phải teacher hoặc student" };
            }
            // Kiểm tra teacher đã tồn trại trong lớp
            if (role!.name_Role == "teacher")
            {
                var class_member = await (from cl in _db.tbClassSchool_Menber
                                          where cl.id_ClassSchool == request.id_ClassSchool
                                          join m in _db.tbMenberSchool
                                          on cl.id_MenberSchool equals m.id_MenberSchool
                                          join r in _db.tbRoleSchool
                                          on m.id_RoleSchool equals r.id_RoleSchool
                                          where r.name_Role == "teacher"
                                          select cl).AnyAsync();

                if (class_member)
                {
                    return new Status_Application { StatusBool = false, StatusType = $"Lớp này đã có teacher" };
                }
            }
            tbClassSchool_Menber classSchool_Menber = new tbClassSchool_Menber
            {
                id_ClassSchool = request.id_ClassSchool,
                id_MenberSchool = request.id_Student,
            };
            return await _class_Member_Repository_V1.Insert(classSchool_Menber);
        }
    }
}
