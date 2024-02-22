using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class add_table_tbSchool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbSchool",
                columns: table => new
                {
                    id_School = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Account = table.Column<int>(type: "int", nullable: false),
                    name_School = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description_School = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adderss_School = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    evaluate_School = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbSchool", x => x.id_School);
                    table.ForeignKey(
                        name: "FK_tbSchool_tbAccount_id_Account",
                        column: x => x.id_Account,
                        principalTable: "tbAccount",
                        principalColumn: "id_Account",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbSchool_id_Account",
                table: "tbSchool",
                column: "id_Account");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbSchool");
        }
    }
}
