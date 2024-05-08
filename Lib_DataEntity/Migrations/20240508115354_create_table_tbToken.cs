using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class create_table_tbToken : Migration
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
                    key_refresh_Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    access_Expire_Token = table.Column<DateTime>(type: "datetime2", nullable: false),
                    refresh_Expire_Token = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_Account = table.Column<int>(type: "int", nullable: false),
                    is_Active = table.Column<bool>(type: "bit", nullable: false),
                    ipv4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ipv6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hostName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    browserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    time_login = table.Column<DateTime>(type: "datetime2", nullable: true)
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
