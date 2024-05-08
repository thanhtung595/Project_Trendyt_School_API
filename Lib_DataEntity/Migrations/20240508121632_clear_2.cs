using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class clear_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "tbRole",
            //    keyColumn: "id_Role",
            //    keyValue: new Guid("01c0ea9c-bdd9-42ba-bb90-3e7dbec1b137"));

            //migrationBuilder.DeleteData(
            //    table: "tbRole",
            //    keyColumn: "id_Role",
            //    keyValue: new Guid("653947d1-1aa2-4624-adc7-fbd35cf5a142"));

            //migrationBuilder.DeleteData(
            //    table: "tbRoleSchool",
            //    keyColumn: "id_RoleSchool",
            //    keyValue: new Guid("48b0622a-7eb4-4b10-88de-b4285768b64b"));

            //migrationBuilder.DeleteData(
            //    table: "tbRoleSchool",
            //    keyColumn: "id_RoleSchool",
            //    keyValue: new Guid("837bc119-e77e-4eb9-bbc6-b09978e8ad48"));

            //migrationBuilder.DeleteData(
            //    table: "tbRoleSchool",
            //    keyColumn: "id_RoleSchool",
            //    keyValue: new Guid("8cd6d7fd-b458-4cdc-afd5-42b0786593f1"));

            //migrationBuilder.DeleteData(
            //    table: "tbRoleSchool",
            //    keyColumn: "id_RoleSchool",
            //    keyValue: new Guid("99fd4ba6-91c7-459f-a0db-0d64604f96ae"));

            //migrationBuilder.DeleteData(
            //    table: "tbRoleSchool",
            //    keyColumn: "id_RoleSchool",
            //    keyValue: new Guid("d0a67264-30dc-4fc9-afd9-55834d64220a"));

            //migrationBuilder.DeleteData(
            //    table: "tbStyleBuoiHoc",
            //    keyColumn: "id_StyleBuoiHoc",
            //    keyValue: 1);

            //migrationBuilder.DeleteData(
            //    table: "tbStyleBuoiHoc",
            //    keyColumn: "id_StyleBuoiHoc",
            //    keyValue: 2);

            //migrationBuilder.DeleteData(
            //    table: "tbStyleBuoiHoc",
            //    keyColumn: "id_StyleBuoiHoc",
            //    keyValue: 3);

            //migrationBuilder.DeleteData(
            //    table: "tbTag",
            //    keyColumn: "id_Tag",
            //    keyValue: 1);

            //migrationBuilder.DeleteData(
            //    table: "tbTag",
            //    keyColumn: "id_Tag",
            //    keyValue: 2);

            //migrationBuilder.DeleteData(
            //    table: "tbTag",
            //    keyColumn: "id_Tag",
            //    keyValue: 3);

            //migrationBuilder.DeleteData(
            //    table: "tbTypeAccount",
            //    keyColumn: "id_TypeAccount",
            //    keyValue: new Guid("79ff0eb5-0a6a-4b21-bd7e-53dc9391527a"));

            //migrationBuilder.DeleteData(
            //    table: "tbTypeAccount",
            //    keyColumn: "id_TypeAccount",
            //    keyValue: new Guid("b60476d5-7ecd-42b4-ae54-0b2653b9a259"));

            //migrationBuilder.DeleteData(
            //    table: "tbTypeAccount",
            //    keyColumn: "id_TypeAccount",
            //    keyValue: new Guid("bf682274-7606-4042-909d-4f3d2f21f662"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.InsertData(
            //    table: "tbRole",
            //    columns: new[] { "id_Role", "name_Role" },
            //    values: new object[,]
            //    {
            //        { new Guid("01c0ea9c-bdd9-42ba-bb90-3e7dbec1b137"), "admin" },
            //        { new Guid("653947d1-1aa2-4624-adc7-fbd35cf5a142"), "guest" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "tbRoleSchool",
            //    columns: new[] { "id_RoleSchool", "name_Role" },
            //    values: new object[,]
            //    {
            //        { new Guid("48b0622a-7eb4-4b10-88de-b4285768b64b"), "student" },
            //        { new Guid("837bc119-e77e-4eb9-bbc6-b09978e8ad48"), "industry management" },
            //        { new Guid("8cd6d7fd-b458-4cdc-afd5-42b0786593f1"), "secretary management" },
            //        { new Guid("99fd4ba6-91c7-459f-a0db-0d64604f96ae"), "school management" },
            //        { new Guid("d0a67264-30dc-4fc9-afd9-55834d64220a"), "teacher" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "tbStyleBuoiHoc",
            //    columns: new[] { "id_StyleBuoiHoc", "name" },
            //    values: new object[,]
            //    {
            //        { 1, "Học binh thường" },
            //        { 2, "Kiểm tra giữ môn" },
            //        { 3, "Kiểm tra cuối môn" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "tbTag",
            //    columns: new[] { "id_Tag", "name" },
            //    values: new object[,]
            //    {
            //        { 1, "active" },
            //        { 2, "delete" },
            //        { 3, "done" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "tbTypeAccount",
            //    columns: new[] { "id_TypeAccount", "name_TypeAccount" },
            //    values: new object[,]
            //    {
            //        { new Guid("79ff0eb5-0a6a-4b21-bd7e-53dc9391527a"), "account name" },
            //        { new Guid("b60476d5-7ecd-42b4-ae54-0b2653b9a259"), "facebook" },
            //        { new Guid("bf682274-7606-4042-909d-4f3d2f21f662"), "gmail" }
            //    });
        }
    }
}
