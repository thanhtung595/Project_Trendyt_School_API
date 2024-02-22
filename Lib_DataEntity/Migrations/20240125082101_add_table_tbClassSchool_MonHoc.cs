using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class add_table_tbClassSchool_MonHoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbClassSchool_MonHoc",
                columns: table => new
                {
                    id_ClassSchool_MonHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_ClassSchool = table.Column<int>(type: "int", nullable: false),
                    _SoBuoiNghi = table.Column<int>(type: "int", nullable: false),
                    id_MonHoc = table.Column<int>(type: "int", nullable: false),
                    _danhGiaTrungBinh = table.Column<float>(type: "real", nullable: false),
                    tags = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbClassSchool_MonHoc_id_ClassSchool",
                table: "tbClassSchool_MonHoc",
                column: "id_ClassSchool");

            migrationBuilder.CreateIndex(
                name: "IX_tbClassSchool_MonHoc_id_MonHoc",
                table: "tbClassSchool_MonHoc",
                column: "id_MonHoc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbClassSchool_MonHoc");
        }
    }
}
