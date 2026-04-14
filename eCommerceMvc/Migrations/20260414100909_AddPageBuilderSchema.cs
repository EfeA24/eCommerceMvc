using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceMvc.Migrations
{
    /// <inheritdoc />
    public partial class AddPageBuilderSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "pages");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "pages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "pages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LayoutJson",
                table: "pages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "pages",
                type: "nvarchar(220)",
                maxLength: 220,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "pages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "pages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_pages_Slug",
                table: "pages",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_pages_Slug",
                table: "pages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "pages");

            migrationBuilder.DropColumn(
                name: "LayoutJson",
                table: "pages");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "pages");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "pages");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "pages");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "pages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "pages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
