﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class addtable_tbLichHoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbLichHoc",
                columns: table => new
                {
                    id_LichHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_MonHoc = table.Column<int>(type: "int", nullable: false),
                    thoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    thoiGianKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbLichHoc", x => x.id_LichHoc);
                    table.ForeignKey(
                        name: "FK_tbLichHoc_tbMonHoc_id_MonHoc",
                        column: x => x.id_MonHoc,
                        principalTable: "tbMonHoc",
                        principalColumn: "id_MonHoc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbLichHoc_id_MonHoc",
                table: "tbLichHoc",
                column: "id_MonHoc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbLichHoc");
        }
    }
}