using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class create_table_tbDiemDanh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbDiemDanh",
                columns: table => new
                {
                    id_DiemDanh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_LichHoc = table.Column<int>(type: "int", nullable: false),
                    id_MonHocClass_Student = table.Column<int>(type: "int", nullable: false),
                    _DauGio = table.Column<bool>(type: "bit", nullable: false,defaultValue:false),
                    _CuoiGio = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    _DiMuon = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbDiemDanh", x => x.id_DiemDanh);
                    table.ForeignKey(
                        name: "FK_tbDiemDanh_tbLichHoc_id_LichHoc",
                        column: x => x.id_LichHoc,
                        principalTable: "tbLichHoc",
                        principalColumn: "id_LichHoc",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tbDiemDanh_tbMonHocClass_Student_id_MonHocClass_Student",
                        column: x => x.id_MonHocClass_Student,
                        principalTable: "tbMonHocClass_Student",
                        principalColumn: "id_MonHocClass_Student",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbDiemDanh_id_LichHoc",
                table: "tbDiemDanh",
                column: "id_LichHoc");

            migrationBuilder.CreateIndex(
                name: "IX_tbDiemDanh_id_MonHocClass_Student",
                table: "tbDiemDanh",
                column: "id_MonHocClass_Student");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbDiemDanh");
        }
    }
}
