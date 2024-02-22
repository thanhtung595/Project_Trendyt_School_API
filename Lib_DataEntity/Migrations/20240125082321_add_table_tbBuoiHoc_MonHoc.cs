using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class add_table_tbBuoiHoc_MonHoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbBuoiHoc_MonHoc",
                columns: table => new
                {
                    id_BuoiHoc_MonHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_ClassSchool_MonHoc = table.Column<int>(type: "int", nullable: false),
                    id_BuoiHoc = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_tbBuoiHoc_MonHoc_id_BuoiHoc",
                table: "tbBuoiHoc_MonHoc",
                column: "id_BuoiHoc");

            migrationBuilder.CreateIndex(
                name: "IX_tbBuoiHoc_MonHoc_id_ClassSchool_MonHoc",
                table: "tbBuoiHoc_MonHoc",
                column: "id_ClassSchool_MonHoc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbBuoiHoc_MonHoc");
        }
    }
}
