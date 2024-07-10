using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class create_table_tbThongBao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbThongBao",
                columns: table => new
                {
                    idThongBao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timeThongBao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idTypeThongBao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbThongBao", x => x.idThongBao);
                    table.ForeignKey(
                        name: "FK_tbThongBao_tbTypeThongBao_idTypeThongBao",
                        column: x => x.idTypeThongBao,
                        principalTable: "tbTypeThongBao",
                        principalColumn: "idTypeThongBao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbFileThongBao",
                columns: table => new
                {
                    idFileThongBao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    file = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idThongBao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbFileThongBao", x => x.idFileThongBao);
                    table.ForeignKey(
                        name: "FK_tbFileThongBao_tbThongBao_idThongBao",
                        column: x => x.idThongBao,
                        principalTable: "tbThongBao",
                        principalColumn: "idThongBao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbMiddlewareThongBao",
                columns: table => new
                {
                    MiddlewareThongBao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMiddleware = table.Column<int>(type: "int", nullable: true),
                    idThongBao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbMiddlewareThongBao", x => x.MiddlewareThongBao);
                    table.ForeignKey(
                        name: "FK_tbMiddlewareThongBao_tbThongBao_idThongBao",
                        column: x => x.idThongBao,
                        principalTable: "tbThongBao",
                        principalColumn: "idThongBao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbFileThongBao_idThongBao",
                table: "tbFileThongBao",
                column: "idThongBao");

            migrationBuilder.CreateIndex(
                name: "IX_tbMiddlewareThongBao_idThongBao",
                table: "tbMiddlewareThongBao",
                column: "idThongBao");

            migrationBuilder.CreateIndex(
                name: "IX_tbThongBao_idTypeThongBao",
                table: "tbThongBao",
                column: "idTypeThongBao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbFileThongBao");

            migrationBuilder.DropTable(
                name: "tbMiddlewareThongBao");

            migrationBuilder.DropTable(
                name: "tbThongBao");
        }
    }
}
