using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class add_table_tbToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbToken",
                columns: table => new
                {
                    id_Token = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    access_Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    refresh_Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    access_Expire_Token = table.Column<DateTime>(type: "datetime2", nullable: false),
                    refresh_Expire_Token = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_Account = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbToken", x => x.id_Token);
                    table.ForeignKey(
                        name: "FK_tbToken_tbAccount_id_Account",
                        column: x => x.id_Account,
                        principalTable: "tbAccount",
                        principalColumn: "id_Account",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbToken_id_Account",
                table: "tbToken",
                column: "id_Account");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbToken");
        }
    }
}
