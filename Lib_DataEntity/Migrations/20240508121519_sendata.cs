using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class sendata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tbRole",
                columns: new[] { "id_Role", "name_Role" },
                values: new object[,]
                {
                    { new Guid("28e07b50-01cb-4999-a05f-2a3b69896230"), "guest" },
                    { new Guid("f554ee77-268e-4a71-a276-d8ceab062f0d"), "admin" }
                });

            migrationBuilder.InsertData(
                table: "tbRoleSchool",
                columns: new[] { "id_RoleSchool", "name_Role" },
                values: new object[,]
                {
                    { new Guid("1d1a894d-5f83-4c72-ab9d-511ddf91219c"), "secretary management" },
                    { new Guid("5477fd27-f67a-40a7-b3f6-a62fc11baa61"), "teacher" },
                    { new Guid("d506d309-9592-4d45-b9eb-f97d1412dbdd"), "student" },
                    { new Guid("d90693e5-4e27-4b59-97a0-c7c71cb0e375"), "industry management" },
                    { new Guid("e8f86a3a-4eb3-4e24-ab41-328ae2fed3fb"), "school management" }
                });

            migrationBuilder.InsertData(
                table: "tbStyleBuoiHoc",
                columns: new[] { "id_StyleBuoiHoc", "name" },
                values: new object[,]
                {
                    { 1, "Học binh thường" },
                    { 2, "Kiểm tra giữ môn" },
                    { 3, "Kiểm tra cuối môn" }
                });

            migrationBuilder.InsertData(
                table: "tbTag",
                columns: new[] { "id_Tag", "name" },
                values: new object[,]
                {
                    { 1, "active" },
                    { 2, "delete" },
                    { 3, "done" }
                });

            migrationBuilder.InsertData(
                table: "tbTypeAccount",
                columns: new[] { "id_TypeAccount", "name_TypeAccount" },
                values: new object[,]
                {
                    { new Guid("4b165201-8787-4e86-83d8-46e2d50fdc12"), "account name" },
                    { new Guid("606f309d-d6b3-415c-9e29-f686f5dd919e"), "facebook" },
                    { new Guid("91dbdc98-e2e5-4ed7-a6fe-75f9196b0571"), "gmail" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tbRole",
                keyColumn: "id_Role",
                keyValue: new Guid("28e07b50-01cb-4999-a05f-2a3b69896230"));

            migrationBuilder.DeleteData(
                table: "tbRole",
                keyColumn: "id_Role",
                keyValue: new Guid("f554ee77-268e-4a71-a276-d8ceab062f0d"));

            migrationBuilder.DeleteData(
                table: "tbRoleSchool",
                keyColumn: "id_RoleSchool",
                keyValue: new Guid("1d1a894d-5f83-4c72-ab9d-511ddf91219c"));

            migrationBuilder.DeleteData(
                table: "tbRoleSchool",
                keyColumn: "id_RoleSchool",
                keyValue: new Guid("5477fd27-f67a-40a7-b3f6-a62fc11baa61"));

            migrationBuilder.DeleteData(
                table: "tbRoleSchool",
                keyColumn: "id_RoleSchool",
                keyValue: new Guid("d506d309-9592-4d45-b9eb-f97d1412dbdd"));

            migrationBuilder.DeleteData(
                table: "tbRoleSchool",
                keyColumn: "id_RoleSchool",
                keyValue: new Guid("d90693e5-4e27-4b59-97a0-c7c71cb0e375"));

            migrationBuilder.DeleteData(
                table: "tbRoleSchool",
                keyColumn: "id_RoleSchool",
                keyValue: new Guid("e8f86a3a-4eb3-4e24-ab41-328ae2fed3fb"));

            migrationBuilder.DeleteData(
                table: "tbStyleBuoiHoc",
                keyColumn: "id_StyleBuoiHoc",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tbStyleBuoiHoc",
                keyColumn: "id_StyleBuoiHoc",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tbStyleBuoiHoc",
                keyColumn: "id_StyleBuoiHoc",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tbTag",
                keyColumn: "id_Tag",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tbTag",
                keyColumn: "id_Tag",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tbTag",
                keyColumn: "id_Tag",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tbTypeAccount",
                keyColumn: "id_TypeAccount",
                keyValue: new Guid("4b165201-8787-4e86-83d8-46e2d50fdc12"));

            migrationBuilder.DeleteData(
                table: "tbTypeAccount",
                keyColumn: "id_TypeAccount",
                keyValue: new Guid("606f309d-d6b3-415c-9e29-f686f5dd919e"));

            migrationBuilder.DeleteData(
                table: "tbTypeAccount",
                keyColumn: "id_TypeAccount",
                keyValue: new Guid("91dbdc98-e2e5-4ed7-a6fe-75f9196b0571"));
        }
    }
}
