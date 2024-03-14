using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Update.MonHoc;
using Lib_Models.Models_Insert.v1.MonHoc;
using Lib_Models.Models_Select.MonHoc;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Repository.V1.MonHoc_Repository;
using Lib_Services.Token_Service;
using Lib_Services.V1.MonHoc_Student_Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.MonHoc
{
    public class MonHoc_Service_v1 : IMonHoc_Service_v1
    {
        private readonly Trendyt_DbContext _db;
        private readonly IToken_Service_v1 _tokenService_V1;
        private readonly IToken_Service_v2 _tokenService_V2;
        private readonly IMonHoc_Repository_v1 _monHoc_Repository_V1;
        private readonly IMonHoc_Student_Service_v1 _monHoc_Student_Service_V1;
        public MonHoc_Service_v1(Trendyt_DbContext db, IToken_Service_v1 tokenService_V1,
            IMonHoc_Repository_v1 monHoc_Repository_V1, IMonHoc_Student_Service_v1 monHoc_Student_Service_V1, IToken_Service_v2 tokenService_V2)
        {
            _db = db;
            _tokenService_V1 = tokenService_V1;
            _monHoc_Repository_V1 = monHoc_Repository_V1;
            _monHoc_Student_Service_V1 = monHoc_Student_Service_V1;
            _tokenService_V2 = tokenService_V2;
        }

        public async Task<Status_Application> Edit(MonHoc_Update_v1 request)
        {
            // Check tên môn học, tag, id_teacher, id_monhoc
            if (string.IsNullOrEmpty(request.name_MonHoc))
            {
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập tên môn học" };
            }
            if (string.IsNullOrEmpty(request.tag))
            {
                request.tag = "active";
            }

            if (request.id_Teacher == 0)
            {
                return new Status_Application { StatusBool = false, StatusType = "Teacher không tồn tại" };
            }
            tbMenberSchool menberManager = await _tokenService_V2.Get_Menber_Token();
            var monHoc = await _db.tbMonHoc.FindAsync(request.id_MonHoc);
            if (monHoc == null)
            {
                return new Status_Application { StatusBool = false, StatusType = "Id môn học không hợp lệ" };
            }

            var nameMonHoc = await _db.tbMonHoc.FirstOrDefaultAsync(x => x.id_School == menberManager.id_School
                            && x.id_MonHoc != monHoc.id_MonHoc && x.name_MonHoc!.ToLower() == request.name_MonHoc.ToLower());

            if (nameMonHoc != null)
            {
                return new Status_Application { StatusBool = false, StatusType = "Tên môn học đã tồn tại" };
            }
            else
            {
                monHoc.name_MonHoc = request.name_MonHoc.Trim();
                monHoc.tags = request.tag;
                monHoc._SoBuoiNghi = request.soBuoiNghi;
                monHoc.ngayBatDau = request.ngayBatDau;
                monHoc.ngayKetThuc = request.ngayKetThuc;
            }

            // Kiểm tra role có phải teacher
            var teacher = await (from mt in _db.tbMonHocClass_Student
                                 where mt.id_MonHoc == monHoc.id_MonHoc
                                 join m in _db.tbMenberSchool
                                 on mt.id_MenberSchool equals m.id_MenberSchool
                                 join r in _db.tbRoleSchool
                                 on m.id_RoleSchool equals r.id_RoleSchool
                                 where r.name_Role == "teacher"
                                 select new { TeacherMH = mt, Role = r }).FirstOrDefaultAsync();
            if (teacher == null )
            {
                MonHocClass_Student_Insert_v1 addTeacher = new MonHocClass_Student_Insert_v1
                {
                    id_MonHoc = monHoc.id_MonHoc,
                    id_Student = request.id_Teacher
                };
                Status_Application statutsTeacher = await _monHoc_Student_Service_V1.Insert(addTeacher);
                if (!statutsTeacher.StatusBool)
                {
                    return statutsTeacher;
                }
            }
            teacher!.TeacherMH.id_MenberSchool = request.id_Teacher;
            await _db.SaveChangesAsync();
            return new Status_Application
            {
                StatusBool = true,
                StatusType = "success"
            };
        }

        public async Task<List<MonHoc_SelectAll_v1>> GetAll()
        {
            tbMenberSchool menberManager = await _tokenService_V2.Get_Menber_Token();
            return await _monHoc_Repository_V1.GetAll(menberManager.id_School);
        }

        public async Task<MonHocSelectById_v1> GetById(int id_MonHoc)
        {
            tbMenberSchool menberManager = await _tokenService_V2.Get_Menber_Token();
            return await _monHoc_Repository_V1.GetById(menberManager.id_School,id_MonHoc);
        }

        public async Task<Status_Application> Insert(tbMonHoc monHoc)
        {
            tbMenberSchool menberManager = await _tokenService_V2.Get_Menber_Token();
            // Check null data
            if (string.IsNullOrEmpty(monHoc.name_MonHoc))
            {
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập tên môn học" };
            }

            // Kiểm tra tên môn học đã tồn tại
            bool isNameMonHoc = await _db.tbMonHoc.AnyAsync(x => x.id_School == menberManager.id_School 
                                && x.name_MonHoc!.ToLower() == monHoc.name_MonHoc.ToLower());

            if (isNameMonHoc)
            {
                return new Status_Application { StatusBool = false, StatusType = "Tên môn học đã tồn tại" };
            }

            tbMonHoc monHocAdd = new tbMonHoc
            {
                id_School = menberManager.id_School,
                name_MonHoc = monHoc.name_MonHoc.Trim(),
                _danhGiaTrungBinh = 5,
                tags = "active",
                _SoBuoiNghi = monHoc._SoBuoiNghi,
                ngayBatDau = monHoc.ngayBatDau,
                ngayKetThuc = monHoc.ngayKetThuc
            };

            return await _monHoc_Repository_V1.Insert(monHocAdd);
        }
    }
}
