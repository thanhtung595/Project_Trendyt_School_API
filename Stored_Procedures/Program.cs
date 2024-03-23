using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Chuỗi kết nối đến cơ sở dữ liệu SQL Server
        string connectionString = "Server=LAPTOP-K7MFJOQI;Database=DataBase_Trendyt_School_API;Trusted_Connection=True;TrustServerCertificate=True;";

        // Câu truy vấn SQL để tạo stored procedure
        string createProcedureQuery = @"
            CREATE PROCEDURE GetRoles
            AS
            BEGIN
                SET NOCOUNT ON;
                SELECT id_Role, name_Role FROM tbRole;
            END";

        // Câu truy vấn SQL để gọi stored procedure
        string callProcedureQuery = "EXEC GetRoles";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand createCommand = new SqlCommand(createProcedureQuery, connection);
            SqlCommand callCommand = new SqlCommand(callProcedureQuery, connection);
            connection.Open();

            // Tạo stored procedure trên SQL Server
            createCommand.ExecuteNonQuery();

            // Gọi stored procedure và đọc kết quả
            SqlDataReader reader = callCommand.ExecuteReader();

            // Đọc từng dòng dữ liệu và hiển thị trên console
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id_Role"]}, Name: {reader["name_Role"]}");
            }

            // Đóng kết nối và giải phóng tài nguyên
            reader.Close();
        }
    }
}
