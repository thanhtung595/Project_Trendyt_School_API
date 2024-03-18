using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class removetable_tbBuoiHoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbBuoiHoc");
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

            migrationBuilder.CreateIndex(
                name: "IX_tbBuoiHoc_id_MonHoc",
                table: "tbBuoiHoc",
                column: "id_MonHoc");
        }
    }
}
