using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class create_table_tbKhoaSchool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbKhoaSchool",
                columns: table => new
                {
                    id_KhoaSchool = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_Khoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ma_Khoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_School = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbKhoaSchool", x => x.id_KhoaSchool);
                    table.ForeignKey(
                        name: "FK_tbKhoaSchool_tbSchool_id_School",
                        column: x => x.id_School,
                        principalTable: "tbSchool",
                        principalColumn: "id_School",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbKhoaSchool_id_School",
                table: "tbKhoaSchool",
                column: "id_School");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbKhoaSchool");
        }
    }
}
