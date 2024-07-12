using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class update_collum_tbBaiTap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbBaiTap_tbLichHoc_id_LichHoc",
                table: "tbBaiTap");

            migrationBuilder.RenameColumn(
                name: "id_LichHoc",
                table: "tbBaiTap",
                newName: "id_MonHoc");

            migrationBuilder.RenameIndex(
                name: "IX_tbBaiTap_id_LichHoc",
                table: "tbBaiTap",
                newName: "IX_tbBaiTap_id_MonHoc");

            migrationBuilder.AddColumn<int>(
                name: "id_MenberSchool",
                table: "tbBaiTap",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbBaiTap_id_MenberSchool",
                table: "tbBaiTap",
                column: "id_MenberSchool");

            migrationBuilder.AddForeignKey(
                name: "FK_tbBaiTap_tbMenberSchool_id_MenberSchool",
                table: "tbBaiTap",
                column: "id_MenberSchool",
                principalTable: "tbMenberSchool",
                principalColumn: "id_MenberSchool",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbBaiTap_tbMonHoc_id_MonHoc",
                table: "tbBaiTap",
                column: "id_MonHoc",
                principalTable: "tbMonHoc",
                principalColumn: "id_MonHoc",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbBaiTap_tbMenberSchool_id_MenberSchool",
                table: "tbBaiTap");

            migrationBuilder.DropForeignKey(
                name: "FK_tbBaiTap_tbMonHoc_id_MonHoc",
                table: "tbBaiTap");

            migrationBuilder.DropIndex(
                name: "IX_tbBaiTap_id_MenberSchool",
                table: "tbBaiTap");

            migrationBuilder.DropColumn(
                name: "id_MenberSchool",
                table: "tbBaiTap");

            migrationBuilder.RenameColumn(
                name: "id_MonHoc",
                table: "tbBaiTap",
                newName: "id_LichHoc");

            migrationBuilder.RenameIndex(
                name: "IX_tbBaiTap_id_MonHoc",
                table: "tbBaiTap",
                newName: "IX_tbBaiTap_id_LichHoc");

            migrationBuilder.AddForeignKey(
                name: "FK_tbBaiTap_tbLichHoc_id_LichHoc",
                table: "tbBaiTap",
                column: "id_LichHoc",
                principalTable: "tbLichHoc",
                principalColumn: "id_LichHoc",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
