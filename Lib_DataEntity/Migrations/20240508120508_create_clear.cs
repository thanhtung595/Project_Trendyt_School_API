using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class create_clear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbClassSchool",
                columns: table => new
                {
                    id_ClassSchool = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_ClassSchool = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_KhoaSchool = table.Column<int>(type: "int", nullable: false),
                    id_Tag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbClassSchool", x => x.id_ClassSchool);
                    table.ForeignKey(
                        name: "FK_tbClassSchool_tbKhoaSchool_id_KhoaSchool",
                        column: x => x.id_KhoaSchool,
                        principalTable: "tbKhoaSchool",
                        principalColumn: "id_KhoaSchool",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbClassSchool_tbTag_id_Tag",
                        column: x => x.id_Tag,
                        principalTable: "tbTag",
                        principalColumn: "id_Tag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbClassSchool_id_KhoaSchool",
                table: "tbClassSchool",
                column: "id_KhoaSchool");

            migrationBuilder.CreateIndex(
                name: "IX_tbClassSchool_id_Tag",
                table: "tbClassSchool",
                column: "id_Tag");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbClassSchool");
        }
    }
}
