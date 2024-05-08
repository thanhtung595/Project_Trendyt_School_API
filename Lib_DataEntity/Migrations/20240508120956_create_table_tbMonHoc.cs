using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class create_table_tbMonHoc : Migration
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
                    id_Tag = table.Column<int>(type: "int", nullable: false),
                    ngayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    _SoBuoiNghi = table.Column<int>(type: "int", nullable: false),
                    _SoBuoiHoc = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_tbMonHoc_tbTag_id_Tag",
                        column: x => x.id_Tag,
                        principalTable: "tbTag",
                        principalColumn: "id_Tag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbMonHoc_id_School",
                table: "tbMonHoc",
                column: "id_School");

            migrationBuilder.CreateIndex(
                name: "IX_tbMonHoc_id_Tag",
                table: "tbMonHoc",
                column: "id_Tag");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbMonHoc");
        }
    }
}
