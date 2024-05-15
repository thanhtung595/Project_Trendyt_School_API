using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using AutoMapper.Execution;
using Azure.Core;
using Lib_Models.Models_Insert.v2.MonHoc;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Repository.Abstract;
using Lib_Repository.Repository_Class;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.MonHocClass_Student
{
    public class MonHocClass_Student_v2 : IMonHocClass_Student_v2
    {
        private readonly Trendyt_DbContext _db;
        private readonly IRepository<tbMonHocClass_Student> _repositoryMonHocClassStudent;
        private readonly IRepository<tbMonHoc> _repositoryMonHoc;
        private readonly IRepository<tbMenberSchool> _repositoryMenberSchool;
        public MonHocClass_Student_v2(Trendyt_DbContext db, IRepository<tbMonHocClass_Student> repositoryMonHocClassStudent,
                                      IRepository<tbMonHoc> repositoryMonHoc, IRepository<tbMenberSchool> repositoryMenberSchool)
        {
            _db = db;
            _repositoryMonHocClassStudent = repositoryMonHocClassStudent;
            _repositoryMonHoc = repositoryMonHoc;
            _repositoryMenberSchool = repositoryMenberSchool;
        }


        public async Task<Status_Application> InsertAsync(int idMonHoc, List<ListStudent_MonHoc_Insert_v2> member)
        {
            using (var dbContextTransaction = new TransactionManager(_db))
            {
                try
                {
                    // Check list add request có trùng
                    List<int> memberRequests = new List<int>();
                    foreach (var item in member)
                    {
                        memberRequests.Add(item.id_Student);
                    }
                    var checkListRequest = CheckForDuplicates(memberRequests);
                    if (checkListRequest != null)
                    {
                        return checkListRequest;
                    }

                    // Check môn học có tồn tại
                    var monHocdb = await _repositoryMonHoc.GetById(idMonHoc);
                    if (monHocdb == null)
                    {
                        return new Status_Application { StatusBool = false, StatusType = "Môn học không tồn tại" };
                    }
                    // Check sinh viên đã tồn tại trong lớp
                    var listStudentMonHoc = await _repositoryMonHocClassStudent.GetAll(x => x.id_MonHoc == idMonHoc);
                    HashSet<int> listIntIdStudent = new HashSet<int>();

                    if (!listStudentMonHoc.Any())
                    {
                        // Add listStudentMonHoc và 1 HashSet
                        foreach (var item in listStudentMonHoc)
                        {
                            listIntIdStudent.Add(item.id_MenberSchool);
                        }

                        // Kiểm tra member add đã tồn tại trong list db
                        foreach (var item_MemberAdd in memberRequests)
                        {
                            if (listIntIdStudent.Contains(item_MemberAdd))
                            {
                                dbContextTransaction.Rollback();
                                return new Status_Application { StatusBool = false, StatusType = $"Sinh viên với MSV: {item_MemberAdd} đã tồn tại" };
                            }
                        }
                    }

                    List<tbMonHocClass_Student> add_Student_MonHocs = new List<tbMonHocClass_Student>();
                    foreach (var item in memberRequests)
                    {
                        var memberDb = await _repositoryMenberSchool.GetAllIncluding(x => x.id_MenberSchool == item, r => r.tbRoleSchool!);
                        if (memberDb == null)
                        {
                            return new Status_Application { StatusBool = false, StatusType = $"MSV: {item} không tồn tại" };
                        }
                        if (memberDb.First().tbRoleSchool!.name_Role != "student")
                        {
                            return new Status_Application { StatusBool = false, StatusType = $"MSV: {item} không phải là student mà là: {memberDb.First().tbRoleSchool!.name_Role}" };
                        }
                        tbMonHocClass_Student add_Student_MonHoc = new tbMonHocClass_Student
                        {
                            id_MenberSchool = item,
                            id_MonHoc = idMonHoc
                        };
                        add_Student_MonHocs.Add(add_Student_MonHoc);
                    }
                    await _repositoryMonHocClassStudent.Insert(add_Student_MonHocs);
                    await _repositoryMonHocClassStudent.Commit();

                    return new Status_Application { StatusBool = true, StatusType = "success" };
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return new Status_Application { StatusBool = false, StatusType = ex.Message };
                }
            }
        }
        public async Task<Status_Application> InsertAsync(int idMonHoc, List<ListStudent_MonHoc_Insert_v2> member, int idTeacher)
        {
            try
            {
                List<tbMonHocClass_Student> add_Student_MonHocs = new List<tbMonHocClass_Student>();
                if (member.Count() > 0)
                {
                    // Check list add request có trùng
                    List<int> memberRequests = new List<int>();
                    foreach (var item in member)
                    {
                        memberRequests.Add(item.id_Student);
                    }
                    var checkListRequest = CheckForDuplicates(memberRequests);
                    if (checkListRequest != null)
                    {
                        return checkListRequest;
                    }


                    foreach (var item in memberRequests)
                    {
                        var memberDb = await _repositoryMenberSchool.GetAllIncluding(x => x.id_MenberSchool == item, r => r.tbRoleSchool!);
                        if (memberDb == null)
                        {
                            return new Status_Application { StatusBool = false, StatusType = $"MSV: {item} không tồn tại" };
                        }
                        if (memberDb.First().tbRoleSchool!.name_Role != "student")
                        {
                            return new Status_Application { StatusBool = false, StatusType = $"MSV: {item} không phải là student mà là: {memberDb.First().tbRoleSchool!.name_Role}" };
                        }
                        tbMonHocClass_Student add_Student_MonHoc = new tbMonHocClass_Student
                        {
                            id_MenberSchool = item,
                            id_MonHoc = idMonHoc
                        };
                        add_Student_MonHocs.Add(add_Student_MonHoc);
                    }
                    await _repositoryMonHocClassStudent.Insert(add_Student_MonHocs);
                }

                // Add Teacher
                var memberTeacherDb = await _repositoryMenberSchool.GetAllIncluding(x => x.id_MenberSchool == idTeacher, r => r.tbRoleSchool!);
                if (memberTeacherDb == null)
                {
                    return new Status_Application { StatusBool = false, StatusType = $"MTC: {idTeacher} không tồn tại" };
                }
                if (memberTeacherDb.First().tbRoleSchool!.name_Role != "teacher")
                {
                    return new Status_Application { StatusBool = false, StatusType = $"MTC: {idTeacher} không phải là teacher mà là: {memberTeacherDb.First().tbRoleSchool!.name_Role}" };
                }
                tbMonHocClass_Student add_Teacher_MonHoc = new tbMonHocClass_Student
                {
                    id_MenberSchool = idTeacher,
                    id_MonHoc = idMonHoc
                };

                await _repositoryMonHocClassStudent.Insert(add_Teacher_MonHoc);

                await _repositoryMonHocClassStudent.Commit();
                List<int> idMemberClases = new List<int>();
                foreach (var item in add_Student_MonHocs)
                {
                    idMemberClases.Add(item.id_MonHocClass_Student);
                }
                return new Status_Application { StatusBool = true, StatusType = "success" , List_Id_Int = idMemberClases };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false, StatusType = ex.Message };
            }
        }
        private static Status_Application CheckForDuplicates(List<int> list)
        {
            // Tạo HashSet để lưu các phần tử đã gặp
            HashSet<int> seenNumbers = new HashSet<int>();

            // Duyệt qua danh sách
            foreach (int number in list)
            {
                // lưu các cặp đã trùng
                if (!seenNumbers.Add(number))
                {
                    return new Status_Application { StatusBool = false, StatusType = $"MSV: {number} trùng nhau" };
                }
            }

            // Nếu không có phần tử nào trùng lặp
            return null!;
        }
    }
}
