using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class send_data_tbTypeThongBao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tbTypeThongBao",
                columns: new[] { "idTypeThongBao", "type" },
                values: new object[,]
                {
                    { 1, "Toàn quản trị viên" },
                    { 2, "Toàn quản giáo viên" },
                    { 3, "Toàn sinh viên" },
                    { 4, "Tham gia lớp mới" },
                    { 5, "Bài tập mới" },
                    { 6, "Chấm điểm bài tập" },
                    { 7, "Sinh viên cụ thể" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tbTypeThongBao",
                keyColumn: "idTypeThongBao",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tbTypeThongBao",
                keyColumn: "idTypeThongBao",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tbTypeThongBao",
                keyColumn: "idTypeThongBao",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tbTypeThongBao",
                keyColumn: "idTypeThongBao",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tbTypeThongBao",
                keyColumn: "idTypeThongBao",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "tbTypeThongBao",
                keyColumn: "idTypeThongBao",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "tbTypeThongBao",
                keyColumn: "idTypeThongBao",
                keyValue: 7);
        }
    }
}
