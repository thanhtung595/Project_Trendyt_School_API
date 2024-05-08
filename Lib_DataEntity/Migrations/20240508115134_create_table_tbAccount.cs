using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class create_table_tbAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbAccount",
                columns: table => new
                {
                    id_Account = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_Name = table.Column<string>(type: "varchar(max)", nullable: false),
                    user_Password = table.Column<string>(type: "varchar(max)", nullable: false),
                    id_Role = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_TypeAccount = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_Delete = table.Column<bool>(type: "bit", nullable: false),
                    is_Ban = table.Column<bool>(type: "bit", nullable: false),
                    Time_Create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OTP = table.Column<string>(type: "varchar(max)", nullable: true),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthday_User = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sex_User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email_User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image_User = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbAccount", x => x.id_Account);
                    table.ForeignKey(
                        name: "FK_tbAccount_tbRole_id_Role",
                        column: x => x.id_Role,
                        principalTable: "tbRole",
                        principalColumn: "id_Role",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbAccount_tbTypeAccount_id_TypeAccount",
                        column: x => x.id_TypeAccount,
                        principalTable: "tbTypeAccount",
                        principalColumn: "id_TypeAccount",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbAccount_id_Role",
                table: "tbAccount",
                column: "id_Role");

            migrationBuilder.CreateIndex(
                name: "IX_tbAccount_id_TypeAccount",
                table: "tbAccount",
                column: "id_TypeAccount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbAccount");
        }
    }
}
