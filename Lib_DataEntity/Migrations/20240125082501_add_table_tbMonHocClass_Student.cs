using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class add_table_tbMonHocClass_Student : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbMonHocClass_Student",
                columns: table => new
                {
                    id_MonHocClass_Student = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_MenberSchool = table.Column<int>(type: "int", nullable: false),
                    id_ClassSchool_MonHoc = table.Column<int>(type: "int", nullable: false)
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbMonHocClass_Student_id_ClassSchool_MonHoc",
                table: "tbMonHocClass_Student",
                column: "id_ClassSchool_MonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_tbMonHocClass_Student_id_MenberSchool",
                table: "tbMonHocClass_Student",
                column: "id_MenberSchool");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbMonHocClass_Student");
        }
    }
}
