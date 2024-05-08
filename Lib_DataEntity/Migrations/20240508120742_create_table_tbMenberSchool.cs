using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class create_table_tbMenberSchool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbMenberSchool",
                columns: table => new
                {
                    id_MenberSchool = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Account = table.Column<int>(type: "int", nullable: false),
                    id_School = table.Column<int>(type: "int", nullable: false),
                    id_KhoaSchool = table.Column<int>(type: "int", nullable: false),
                    id_RoleSchool = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    danhGiaTb = table.Column<float>(type: "real", nullable: false),
                    id_Tag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbMenberSchool", x => x.id_MenberSchool);
                    table.ForeignKey(
                        name: "FK_tbMenberSchool_tbAccount_id_Account",
                        column: x => x.id_Account,
                        principalTable: "tbAccount",
                        principalColumn: "id_Account",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbMenberSchool_tbRoleSchool_id_RoleSchool",
                        column: x => x.id_RoleSchool,
                        principalTable: "tbRoleSchool",
                        principalColumn: "id_RoleSchool",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbMenberSchool_tbSchool_id_School",
                        column: x => x.id_School,
                        principalTable: "tbSchool",
                        principalColumn: "id_School",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tbMenberSchool_tbTag_id_Tag",
                        column: x => x.id_Tag,
                        principalTable: "tbTag",
                        principalColumn: "id_Tag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbMenberSchool_id_Account",
                table: "tbMenberSchool",
                column: "id_Account");

            migrationBuilder.CreateIndex(
                name: "IX_tbMenberSchool_id_RoleSchool",
                table: "tbMenberSchool",
                column: "id_RoleSchool");

            migrationBuilder.CreateIndex(
                name: "IX_tbMenberSchool_id_School",
                table: "tbMenberSchool",
                column: "id_School");

            migrationBuilder.CreateIndex(
                name: "IX_tbMenberSchool_id_Tag",
                table: "tbMenberSchool",
                column: "id_Tag");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbMenberSchool");
        }
    }
}
