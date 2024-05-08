using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class create_table_tbClassSchool_Menber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbClassSchool_Menber",
                columns: table => new
                {
                    id_MonHocClass_Student = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_MenberSchool = table.Column<int>(type: "int", nullable: false),
                    id_ClassSchool = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbClassSchool_Menber", x => x.id_MonHocClass_Student);
                    table.ForeignKey(
                        name: "FK_tbClassSchool_Menber_tbClassSchool_id_ClassSchool",
                        column: x => x.id_ClassSchool,
                        principalTable: "tbClassSchool",
                        principalColumn: "id_ClassSchool",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbClassSchool_Menber_tbMenberSchool_id_MenberSchool",
                        column: x => x.id_MenberSchool,
                        principalTable: "tbMenberSchool",
                        principalColumn: "id_MenberSchool",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbClassSchool_Menber_id_ClassSchool",
                table: "tbClassSchool_Menber",
                column: "id_ClassSchool");

            migrationBuilder.CreateIndex(
                name: "IX_tbClassSchool_Menber_id_MenberSchool",
                table: "tbClassSchool_Menber",
                column: "id_MenberSchool");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbClassSchool_Menber");
        }
    }
}
