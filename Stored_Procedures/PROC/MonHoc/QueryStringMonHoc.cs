using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stored_Procedures.PROC.MonHoc
{
    public static class QueryStringMonHoc
    {
        public static string CheckIsNotNullGetAllMonHocPROC = @"
                        DECLARE @procExists INT

                        IF OBJECT_ID('GetAllMonHocPROC', 'P') IS NOT NULL
                        BEGIN
                            PRINT 'GetAllMonHocPROC đã tồn tại'
                            SET @procExists = 1
                        END
                        ELSE
                        BEGIN
                            SET @procExists = 0
                        END

                        SELECT @procExists";

        public static string CreateProcGetAllMonHocPROC = @"
                            CREATE PROCEDURE GetAllMonHocPROC
                                @idSchool INT
                            AS
                            BEGIN
                                SELECT mh.id_MonHoc,
                                       mh.name_MonHoc,
                                       mh._danhGiaTrungBinh,
                                       mh.tags AS tag,
                                       mh._SoBuoiNghi AS soBuoiNghi,
                                       mh.ngayBatDau,
                                       mh.ngayKetThuc,
                                       t.id_MenberSchool AS id_Teacher,
                                       ac.user_Name,
                                       ac.fullName,
                                       k.name_Khoa,
                                       k.ma_Khoa,
                                       ac.image_User AS image_User_Teacher,
                                       m.tags AS tag_Teacher,
                                       (SELECT COUNT(*)
                                        FROM tbMonHocClass_Student st
                                        INNER JOIN tbMenberSchool m ON st.id_MenberSchool = m.id_MenberSchool
                                        INNER JOIN tbRoleSchool r ON m.id_RoleSchool = r.id_RoleSchool
                                        WHERE st.id_MonHoc = mh.id_MonHoc AND r.name_Role = 'student') AS countStudent,
                                       s.id_MenberSchool AS id_Student,
                                       ac_student.user_Name AS user_Name_Student,
                                       ac_student.fullName AS fullName_Student,
                                       k_student.name_Khoa AS name_Khoa_Student,
                                       k_student.ma_Khoa AS ma_Khoa_Student,
                                       ac_student.image_User AS image_User_Student,
                                       ac_student.email_User AS email_User_Student,
                                       ac_student.phone_User AS phone_User_Student,
                                       ac_student.sex_User AS sex_User_Student
                                FROM tbMonHoc mh
                                LEFT JOIN tbMonHocClass_Student st ON mh.id_MonHoc = st.id_MonHoc
                                LEFT JOIN tbMenberSchool m ON st.id_MenberSchool = m.id_MenberSchool
                                LEFT JOIN tbRoleSchool r ON m.id_RoleSchool = r.id_RoleSchool
                                LEFT JOIN tbAccount ac ON m.id_Account = ac.id_Account
                                LEFT JOIN tbKhoaSchool k ON m.id_KhoaSchool = k.id_KhoaSchool
                                LEFT JOIN tbMonHocClass_Student st_teacher ON mh.id_MonHoc = st_teacher.id_MonHoc
                                LEFT JOIN tbMenberSchool t ON st_teacher.id_MenberSchool = t.id_MenberSchool
                                LEFT JOIN tbRoleSchool r_teacher ON t.id_RoleSchool = r_teacher.id_RoleSchool
                                LEFT JOIN tbAccount ac_teacher ON t.id_Account = ac_teacher.id_Account
                                LEFT JOIN tbKhoaSchool k_teacher ON t.id_KhoaSchool = k_teacher.id_KhoaSchool
                                LEFT JOIN tbMonHocClass_Student st_student ON mh.id_MonHoc = st_student.id_MonHoc
                                LEFT JOIN tbMenberSchool s ON st_student.id_MenberSchool = s.id_MenberSchool
                                LEFT JOIN tbRoleSchool r_student ON s.id_RoleSchool = r_student.id_RoleSchool
                                LEFT JOIN tbAccount ac_student ON s.id_Account = ac_student.id_Account
                                LEFT JOIN tbKhoaSchool k_student ON s.id_KhoaSchool = k_student.id_KhoaSchool
                                WHERE mh.id_School = @idSchool
                            END
                            ";
    }
}
