using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzureTeacherStudentSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ArchiveDate",
                table: "Groups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Groups",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchiveDate",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Groups");
        }
    }
}
