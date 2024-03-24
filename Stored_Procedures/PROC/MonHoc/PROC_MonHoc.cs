using Lib_Models.Models_Select.MonHoc;
using Stored_Procedures.DataConnectionMSSQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stored_Procedures.PROC.Role;
using Lib_Models.Models_Select.Student;
using Lib_Models.Models_Select.Teacher;

namespace Stored_Procedures.PROC.MonHoc
{
    public class PROC_MonHoc : IPROC_MonHoc
    {
        private readonly string _connectionString;

        public PROC_MonHoc()
        {
            _connectionString = SQLServerConnectString.ConnectionString;
        }

        public bool CreateProc_GetAllMonHocPROC()
        {
            try
            {
                // Kiểm tra xem stored procedure "GetRoles" đã tồn tại hay chưa
                string checkExistenceQuery = QueryStringMonHoc.CheckIsNotNullGetAllMonHocPROC;

                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand(checkExistenceQuery, connection))
                {
                    connection.Open();

                    int procExists = (int)command.ExecuteScalar();

                    // Nếu stored procedure đã tồn tại, thoát và trả về false
                    if (procExists == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("eror: PROC GetAllMonHocPROC đã tồn tại");
                        Console.ResetColor();
                        return false;
                    }

                    // Câu truy vấn SQL để tạo stored procedure
                    string createProcedureQuery = QueryStringMonHoc.CreateProcGetAllMonHocPROC;

                    command.CommandText = createProcedureQuery;
                    command.ExecuteNonQuery();
                }

                // Trả về true nếu tạo stored procedure thành công
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("success: Tạo stored procedure GetAllMonHocPROC thành công");
                Console.ResetColor();
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và thông báo ra cho người dùng hoặc ghi log
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("error: creating or checking stored procedure: " + ex.Message);
                Console.ResetColor();
                return false;
            }
        }

        public async Task<List<MonHoc_SelectAll_v1>> GetAllMonHocPROC(int idSchool)
        {
            List<MonHoc_SelectAll_v1> result = new List<MonHoc_SelectAll_v1>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetAllMonHocPROC", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idSchool", idSchool);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            MonHoc_SelectAll_v1 item = new MonHoc_SelectAll_v1
                            {
                                id_MonHoc = reader.GetInt32(reader.GetOrdinal("id_MonHoc")),
                                name_MonHoc = reader.GetString(reader.GetOrdinal("name_MonHoc")),
                                danhGiaTrungBinh = reader.GetFloat(reader.GetOrdinal("_danhGiaTrungBinh")),
                                tag = reader.GetString(reader.GetOrdinal("tag")),
                                soBuoiNghi = reader.GetInt32(reader.GetOrdinal("soBuoiNghi")),
                                ngayBatDau = reader.GetDateTime(reader.GetOrdinal("ngayBatDau")),
                                ngayKetThuc = reader.GetDateTime(reader.GetOrdinal("ngayKetThuc")),
                                giangvien = new Select_One_Teacher_v1
                                {
                                    id_Teacher = reader.GetInt32(reader.GetOrdinal("id_Teacher")),
                                    user_Name = reader.GetString(reader.GetOrdinal("user_Name")),
                                    fullName = reader.GetString(reader.GetOrdinal("fullName")),
                                    name_Khoa = reader.GetString(reader.GetOrdinal("name_Khoa")),
                                    ma_Khoa = reader.GetString(reader.GetOrdinal("ma_Khoa")),
                                    image_User = reader.GetString(reader.GetOrdinal("image_User_Teacher")),
                                    tag = reader.GetString(reader.GetOrdinal("tag_Teacher"))
                                },
                                coutnStudent = reader.GetInt32(reader.GetOrdinal("countStudent")),
                                //student = new Student_Select_v1
                                //{
                                //    id_Student = reader.GetInt32(reader.GetOrdinal("id_Student")),
                                //    user_Name = reader.GetString(reader.GetOrdinal("user_Name_Student")),
                                //    fullName = reader.GetString(reader.GetOrdinal("fullName_Student")),
                                //    name_Khoa = reader.GetString(reader.GetOrdinal("name_Khoa_Student")),
                                //    ma_Khoa = reader.GetString(reader.GetOrdinal("ma_Khoa_Student")),
                                //    image_User = reader.GetString(reader.GetOrdinal("image_User_Student")),
                                //    email_User = reader.GetString(reader.GetOrdinal("email_User_Student")),
                                //    phone_User = reader.GetString(reader.GetOrdinal("phone_User_Student")),
                                //    sex_User = reader.GetString(reader.GetOrdinal("sex_User_Student"))
                                //}
                            };
                            result.Add(item);
                        }
                    }
                }
            }

            return result;
        }
    }
}
