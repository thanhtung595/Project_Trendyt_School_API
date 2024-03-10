using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class deletetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbBuoiHoc_MonHoc");

            migrationBuilder.DropTable(
                name: "tbMonHocClass_Student");

            migrationBuilder.DropTable(
                name: "tbBuoiHoc");

            migrationBuilder.DropTable(
                name: "tbClassSchool_MonHoc");

            migrationBuilder.DropTable(
                name: "tbMonHoc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbBuoiHoc",
                columns: table => new
                {
                    id_BuoiHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    create_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    name_BuoiHoc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbBuoiHoc", x => x.id_BuoiHoc);
                });

            migrationBuilder.CreateTable(
                name: "tbMonHoc",
                columns: table => new
                {
                    id_MonHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_School = table.Column<int>(type: "int", nullable: false),
                    name_MonHoc = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "tbClassSchool_MonHoc",
                columns: table => new
                {
                    id_ClassSchool_MonHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_ClassSchool = table.Column<int>(type: "int", nullable: false),
                    id_MonHoc = table.Column<int>(type: "int", nullable: false),
                    _SoBuoiNghi = table.Column<int>(type: "int", nullable: false),
                    _danhGiaTrungBinh = table.Column<float>(type: "real", nullable: false),
                    tags = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbClassSchool_MonHoc", x => x.id_ClassSchool_MonHoc);
                    table.ForeignKey(
                        name: "FK_tbClassSchool_MonHoc_tbClassSchool_id_ClassSchool",
                        column: x => x.id_ClassSchool,
                        principalTable: "tbClassSchool",
                        principalColumn: "id_ClassSchool",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbClassSchool_MonHoc_tbMonHoc_id_MonHoc",
                        column: x => x.id_MonHoc,
                        principalTable: "tbMonHoc",
                        principalColumn: "id_MonHoc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbBuoiHoc_MonHoc",
                columns: table => new
                {
                    id_BuoiHoc_MonHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_BuoiHoc = table.Column<int>(type: "int", nullable: false),
                    id_ClassSchool_MonHoc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbBuoiHoc_MonHoc", x => x.id_BuoiHoc_MonHoc);
                    table.ForeignKey(
                        name: "FK_tbBuoiHoc_MonHoc_tbBuoiHoc_id_BuoiHoc",
                        column: x => x.id_BuoiHoc,
                        principalTable: "tbBuoiHoc",
                        principalColumn: "id_BuoiHoc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbBuoiHoc_MonHoc_tbClassSchool_MonHoc_id_ClassSchool_MonHoc",
                        column: x => x.id_ClassSchool_MonHoc,
                        principalTable: "tbClassSchool_MonHoc",
                        principalColumn: "id_ClassSchool_MonHoc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbMonHocClass_Student",
                columns: table => new
                {
                    id_MonHocClass_Student = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_ClassSchool_MonHoc = table.Column<int>(type: "int", nullable: false),
                    id_MenberSchool = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbMonHocClass_Student", x => x.id_MonHocClass_Student);
                    table.ForeignKey(
                        name: "FK_tbMonHocClass_Student_tbClassSchool_MonHoc_id_ClassSchool_MonHoc",
                        column: x => x.id_ClassSchool_MonHoc,
                        principalTable: "tbClassSchool_MonHoc",
                        principalColumn: "id_ClassSchool_MonHoc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbMonHocClass_Student_tbMenberSchool_id_MenberSchool",
                        column: x => x.id_MenberSchool,
                        principalTable: "tbMenberSchool",
                        principalColumn: "id_MenberSchool",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbBuoiHoc_MonHoc_id_BuoiHoc",
                table: "tbBuoiHoc_MonHoc",
                column: "id_BuoiHoc");

            migrationBuilder.CreateIndex(
                name: "IX_tbBuoiHoc_MonHoc_id_ClassSchool_MonHoc",
                table: "tbBuoiHoc_MonHoc",
                column: "id_ClassSchool_MonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_tbClassSchool_MonHoc_id_ClassSchool",
                table: "tbClassSchool_MonHoc",
                column: "id_ClassSchool");

            migrationBuilder.CreateIndex(
                name: "IX_tbClassSchool_MonHoc_id_MonHoc",
                table: "tbClassSchool_MonHoc",
                column: "id_MonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_tbMonHoc_id_School",
                table: "tbMonHoc",
                column: "id_School");

            migrationBuilder.CreateIndex(
                name: "IX_tbMonHocClass_Student_id_ClassSchool_MonHoc",
                table: "tbMonHocClass_Student",
                column: "id_ClassSchool_MonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_tbMonHocClass_Student_id_MenberSchool",
                table: "tbMonHocClass_Student",
                column: "id_MenberSchool");
        }
    }
}
