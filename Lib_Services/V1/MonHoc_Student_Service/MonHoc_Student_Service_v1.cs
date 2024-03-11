using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Models_Insert.v1.MonHoc;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Repository.V1.MonHoc_Student_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.MonHoc_Student_Service
{
    public class MonHoc_Student_Service_v1 : IMonHoc_Student_Service_v1
    {
        private readonly IMonHoc_Student_Repository_v1 _monHoc_Student_Repository_V1;
        private readonly Trendyt_DbContext _db;
        public MonHoc_Student_Service_v1(Trendyt_DbContext db, IMonHoc_Student_Repository_v1 monHoc_Student_Repository_V1)
        {
            _db = db;
            _monHoc_Student_Repository_V1 = monHoc_Student_Repository_V1;
        }
        public async Task<Status_Application> Insert(MonHocClass_Student_Insert_v1 request)
        {
            var monhoc = await _db.tbMonHoc.FindAsync(request.id_MonHoc);
            // Ckeck mon hoc
            if (monhoc == null)
            {
                return new Status_Application { StatusBool = false, StatusType = $"Môn học với id {request.id_MonHoc} không tồn tại" };
            }
            // Check student đã tồn tại
            var isStudent = await _db.tbMonHocClass_Student.FirstOrDefaultAsync(x => x.id_MonHoc == request.id_MonHoc 
                            && x.id_MenberSchool == request.id_Student);
            if (isStudent != null)
            {
                return new Status_Application { StatusBool = false, StatusType = $"Student id {request.id_Student} đã có trong môn học" };
            }

            // Check student
            var student = await (from m in _db.tbMenberSchool
                                 where m.id_MenberSchool == request.id_Student
                                 join r in _db.tbRoleSchool
                                 on m.id_RoleSchool equals r.id_RoleSchool
                                 select new { studentMonHoc = m, role = r }).FirstOrDefaultAsync();

            if (student == null)
            {
                return new Status_Application { StatusBool = false, StatusType = $"Student id {request.id_Student} không tồn tại" };
            }
            if (student.role.name_Role != "student" && student.role.name_Role != "teacher")
            {
                return new Status_Application { StatusBool = false, StatusType = $"id {request.id_Student} không phải teacher hoặc student" };
            }

            if (student.role.name_Role == "teacher")
            {
                // Check teacher
                var teacher = await (from ms in _db.tbMonHocClass_Student
                                     where ms.id_MonHoc == request.id_MonHoc
                                     join m in _db.tbMenberSchool
                                     on ms.id_MenberSchool equals m.id_MenberSchool
                                     join r in _db.tbRoleSchool
                                     on m.id_RoleSchool equals r.id_RoleSchool
                                     where r.name_Role == "teacher"
                                     select new { studentMonHoc = ms, role = r }).FirstOrDefaultAsync();
                if (teacher != null)
                {
                    return new Status_Application { StatusBool = false, StatusType = "Môn học này đã tồn tại giảng viên" };
                }
            }
            tbMonHocClass_Student hocClass_Student = new tbMonHocClass_Student
            {
                id_MenberSchool = request.id_Student,
                id_MonHoc = request.id_MonHoc,
            };

            return await _monHoc_Student_Repository_V1.Insert(hocClass_Student);
        }
    }
}
