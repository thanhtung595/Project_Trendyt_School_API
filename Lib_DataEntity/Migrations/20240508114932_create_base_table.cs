using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class create_base_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbRole",
                columns: table => new
                {
                    id_Role = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name_Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbRole", x => x.id_Role);
                });

            migrationBuilder.CreateTable(
                name: "tbRoleSchool",
                columns: table => new
                {
                    id_RoleSchool = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name_Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbRoleSchool", x => x.id_RoleSchool);
                });

            migrationBuilder.CreateTable(
                name: "tbStyleBuoiHoc",
                columns: table => new
                {
                    id_StyleBuoiHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbStyleBuoiHoc", x => x.id_StyleBuoiHoc);
                });

            migrationBuilder.CreateTable(
                name: "tbTag",
                columns: table => new
                {
                    id_Tag = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbTag", x => x.id_Tag);
                });

            migrationBuilder.CreateTable(
                name: "tbTypeAccount",
                columns: table => new
                {
                    id_TypeAccount = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name_TypeAccount = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbTypeAccount", x => x.id_TypeAccount);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbRole");

            migrationBuilder.DropTable(
                name: "tbRoleSchool");

            migrationBuilder.DropTable(
                name: "tbStyleBuoiHoc");

            migrationBuilder.DropTable(
                name: "tbTag");

            migrationBuilder.DropTable(
                name: "tbTypeAccount");
        }
    }
}
