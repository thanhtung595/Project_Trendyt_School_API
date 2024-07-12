using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class create_table_tbBaiTap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbBaiTap",
                columns: table => new
                {
                    idBaiTap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameBaiTap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hanNopBai = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_LichHoc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbBaiTap", x => x.idBaiTap);
                    table.ForeignKey(
                        name: "FK_tbBaiTap_tbLichHoc_id_LichHoc",
                        column: x => x.id_LichHoc,
                        principalTable: "tbLichHoc",
                        principalColumn: "id_LichHoc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbFileBaiTap",
                columns: table => new
                {
                    idFileBaiTap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    file = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idBaiTap = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbFileBaiTap", x => x.idFileBaiTap);
                    table.ForeignKey(
                        name: "FK_tbFileBaiTap_tbBaiTap_idBaiTap",
                        column: x => x.idBaiTap,
                        principalTable: "tbBaiTap",
                        principalColumn: "idBaiTap",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbBaiTap_id_LichHoc",
                table: "tbBaiTap",
                column: "id_LichHoc");

            migrationBuilder.CreateIndex(
                name: "IX_tbFileBaiTap_idBaiTap",
                table: "tbFileBaiTap",
                column: "idBaiTap");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbFileBaiTap");

            migrationBuilder.DropTable(
                name: "tbBaiTap");
        }
    }
}
