using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stash.Project.Migrations
{
    /// <inheritdoc />
    public partial class upd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "USer_Remarks",
                table: "UserInfo",
                newName: "User_Remarks");

            migrationBuilder.AlterColumn<string>(
                name: "User_Telephone",
                table: "UserInfo",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "User_RealName",
                table: "UserInfo",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "User_Password",
                table: "UserInfo",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "User_Name",
                table: "UserInfo",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "User_Mobilephone",
                table: "UserInfo",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "User_JobNumber",
                table: "UserInfo",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "User_Email",
                table: "UserInfo",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "User_Remarks",
                table: "UserInfo",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Sector_Name",
                table: "SectorInfo",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "Sector_FatherId",
                table: "SectorInfo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "Sector_CreateTime",
                table: "SectorInfo",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Sector_Remark",
                table: "SectorInfo",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sector_CreateTime",
                table: "SectorInfo");

            migrationBuilder.DropColumn(
                name: "Sector_Remark",
                table: "SectorInfo");

            migrationBuilder.RenameColumn(
                name: "User_Remarks",
                table: "UserInfo",
                newName: "USer_Remarks");

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "User_Telephone",
                keyValue: null,
                column: "User_Telephone",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "User_Telephone",
                table: "UserInfo",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "USer_Remarks",
                keyValue: null,
                column: "USer_Remarks",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "USer_Remarks",
                table: "UserInfo",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "User_RealName",
                keyValue: null,
                column: "User_RealName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "User_RealName",
                table: "UserInfo",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "User_Password",
                keyValue: null,
                column: "User_Password",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "User_Password",
                table: "UserInfo",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "User_Name",
                keyValue: null,
                column: "User_Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "User_Name",
                table: "UserInfo",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "User_Mobilephone",
                keyValue: null,
                column: "User_Mobilephone",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "User_Mobilephone",
                table: "UserInfo",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "User_JobNumber",
                keyValue: null,
                column: "User_JobNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "User_JobNumber",
                table: "UserInfo",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "User_Email",
                keyValue: null,
                column: "User_Email",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "User_Email",
                table: "UserInfo",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "SectorInfo",
                keyColumn: "Sector_Name",
                keyValue: null,
                column: "Sector_Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Sector_Name",
                table: "SectorInfo",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Sector_FatherId",
                table: "SectorInfo",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
