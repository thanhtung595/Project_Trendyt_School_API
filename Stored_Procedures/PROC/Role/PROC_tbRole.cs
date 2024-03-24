using Lib_Models.Models_Select.Role;
using Stored_Procedures.DataConnectionMSSQL;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Stored_Procedures.PROC.Role
{
    public class PROC_tbRole : IPROC_tbRole
    {
        private readonly string _connectionString;

        public PROC_tbRole()
        {
            _connectionString = SQLServerConnectString.ConnectionString;
        }

        public bool CreateProc_GetRoles()
        {
            try
            {
                // Kiểm tra xem stored procedure "GetRoles" đã tồn tại hay chưa
                string checkExistenceQuery = QueryStringRole.CheckIsNotNullGetRoles;

                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand(checkExistenceQuery, connection))
                {
                    connection.Open();

                    int procExists = (int)command.ExecuteScalar();

                    // Nếu stored procedure đã tồn tại, thoát và trả về false
                    if (procExists == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("eror: PROC GetRoles đã tồn tại");
                        Console.ResetColor();
                        return false;
                    }

                    // Câu truy vấn SQL để tạo stored procedure
                    string createProcedureQuery = QueryStringRole.CreateProcGetRoles;

                    command.CommandText = createProcedureQuery;
                    command.ExecuteNonQuery();
                }

                // Trả về true nếu tạo stored procedure thành công
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("success: Tạo stored procedure thành công");
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

        public List<Role_Select_v1> Proc_GetAllRole()
        {
            List<Role_Select_v1> tbRoles = new List<Role_Select_v1>();

            // Kiểm tra xem stored procedure "GetRoles" đã tồn tại hay chưa
            string checkExistenceQuery = QueryStringRole.CheckIsNotNullGetRoles;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(checkExistenceQuery, connection))
            {
                connection.Open();
                int procExists = (int)command.ExecuteScalar();

                // Nếu stored procedure đã tồn tại, thoát và trả về false
                if (procExists == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("warn: PROC GetRoles khong ton tai");
                    Console.ResetColor();
                    return null!;
                }

                command.CommandText = "GetRoles";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string? nameRole = reader.IsDBNull(reader.GetOrdinal("name_Role")) ? null : reader.GetString(reader.GetOrdinal("name_Role"));
                        tbRoles.Add(new Role_Select_v1() { name_Role = nameRole });
                    }
                }
            }
            return tbRoles;
        }
    }
}
