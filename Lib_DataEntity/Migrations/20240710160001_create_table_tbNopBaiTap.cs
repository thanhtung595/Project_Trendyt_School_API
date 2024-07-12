using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class create_table_tbNopBaiTap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbNopBaiTap",
                columns: table => new
                {
                    idNopBaiTap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idBaiTap = table.Column<int>(type: "int", nullable: false),
                    id_MonHocClass_Student = table.Column<int>(type: "int", nullable: false),
                    diem = table.Column<float>(type: "real", nullable: false),
                    danhGia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbNopBaiTap", x => x.idNopBaiTap);
                    table.ForeignKey(
                        name: "FK_tbNopBaiTap_tbBaiTap_idBaiTap",
                        column: x => x.idBaiTap,
                        principalTable: "tbBaiTap",
                        principalColumn: "idBaiTap",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbNopBaiTap_tbMonHocClass_Student_id_MonHocClass_Student",
                        column: x => x.id_MonHocClass_Student,
                        principalTable: "tbMonHocClass_Student",
                        principalColumn: "id_MonHocClass_Student",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "tbFileNopBaiTap",
                columns: table => new
                {
                    idFileNopBaiTap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    file = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idNopBaiTap = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbFileNopBaiTap", x => x.idFileNopBaiTap);
                    table.ForeignKey(
                        name: "FK_tbFileNopBaiTap_tbNopBaiTap_idNopBaiTap",
                        column: x => x.idNopBaiTap,
                        principalTable: "tbNopBaiTap",
                        principalColumn: "idNopBaiTap",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbFileNopBaiTap_idNopBaiTap",
                table: "tbFileNopBaiTap",
                column: "idNopBaiTap");

            migrationBuilder.CreateIndex(
                name: "IX_tbNopBaiTap_id_MonHocClass_Student",
                table: "tbNopBaiTap",
                column: "id_MonHocClass_Student");

            migrationBuilder.CreateIndex(
                name: "IX_tbNopBaiTap_idBaiTap",
                table: "tbNopBaiTap",
                column: "idBaiTap");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbFileNopBaiTap");

            migrationBuilder.DropTable(
                name: "tbNopBaiTap");
        }
    }
}
