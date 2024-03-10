using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class add_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbMonHoc",
                columns: table => new
                {
                    id_MonHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_School = table.Column<int>(type: "int", nullable: false),
                    name_MonHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _danhGiaTrungBinh = table.Column<float>(type: "real", nullable: false),
                    tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    _SoBuoiNghi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbMonHoc", x => x.id_MonHoc);
                    table.ForeignKey(
                        name: "FK_tbMonHoc_tbSchool_id_School",
                        column: x => x.id_School,
                        principalTable: "tbSchool",
                        principalColumn: "id_School",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbBuoiHoc",
                columns: table => new
                {
                    id_BuoiHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_MonHoc = table.Column<int>(type: "int", nullable: false),
                    thoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    thoiGianKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbBuoiHoc", x => x.id_BuoiHoc);
                    table.ForeignKey(
                        name: "FK_tbBuoiHoc_tbMonHoc_id_MonHoc",
                        column: x => x.id_MonHoc,
                        principalTable: "tbMonHoc",
                        principalColumn: "id_MonHoc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbMonHocClass_Student",
                columns: table => new
                {
                    id_MonHocClass_Student = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_MenberSchool = table.Column<int>(type: "int", nullable: false),
                    id_MonHoc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbMonHocClass_Student", x => x.id_MonHocClass_Student);
                    table.ForeignKey(
                        name: "FK_tbMonHocClass_Student_tbMenberSchool_id_MenberSchool",
                        column: x => x.id_MenberSchool,
                        principalTable: "tbMenberSchool",
                        principalColumn: "id_MenberSchool",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tbMonHocClass_Student_tbMonHoc_id_MonHoc",
                        column: x => x.id_MonHoc,
                        principalTable: "tbMonHoc",
                        principalColumn: "id_MonHoc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbBuoiHoc_id_MonHoc",
                table: "tbBuoiHoc",
                column: "id_MonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_tbMonHoc_id_School",
                table: "tbMonHoc",
                column: "id_School");

            migrationBuilder.CreateIndex(
                name: "IX_tbMonHocClass_Student_id_MenberSchool",
                table: "tbMonHocClass_Student",
                column: "id_MenberSchool");

            migrationBuilder.CreateIndex(
                name: "IX_tbMonHocClass_Student_id_MonHoc",
                table: "tbMonHocClass_Student",
                column: "id_MonHoc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbBuoiHoc");

            migrationBuilder.DropTable(
                name: "tbMonHocClass_Student");

            migrationBuilder.DropTable(
                name: "tbMonHoc");
        }
    }
}
