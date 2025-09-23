using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMS.Migrations
{
    /// <inheritdoc />
    public partial class updatebook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminProfileId",
                table: "BorrowedBooks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_AdminProfileId",
                table: "BorrowedBooks",
                column: "AdminProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Users_AdminProfileId",
                table: "BorrowedBooks",
                column: "AdminProfileId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Users_AdminProfileId",
                table: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_AdminProfileId",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "AdminProfileId",
                table: "BorrowedBooks");
        }
    }
}
