using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class update_tbMenberSchool_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_School",
                table: "tbMenberSchool",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbMenberSchool_id_School",
                table: "tbMenberSchool",
                column: "id_School");

            migrationBuilder.AddForeignKey(
                name: "FK_tbMenberSchool_tbSchool_id_School",
                table: "tbMenberSchool",
                column: "id_School",
                principalTable: "tbSchool",
                principalColumn: "id_School",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbMenberSchool_tbSchool_id_School",
                table: "tbMenberSchool");

            migrationBuilder.DropIndex(
                name: "IX_tbMenberSchool_id_School",
                table: "tbMenberSchool");

            migrationBuilder.DropColumn(
                name: "id_School",
                table: "tbMenberSchool");
        }
    }
}
