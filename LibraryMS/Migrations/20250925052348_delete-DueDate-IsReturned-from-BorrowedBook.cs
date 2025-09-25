using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMS.Migrations
{
    /// <inheritdoc />
    public partial class deleteDueDateIsReturnedfromBorrowedBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "BorrowedBooks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "BorrowedBooks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "BorrowedBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
