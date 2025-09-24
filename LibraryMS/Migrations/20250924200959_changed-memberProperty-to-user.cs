using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMS.Migrations
{
    /// <inheritdoc />
    public partial class changedmemberPropertytouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Users_AdminProfileId",
                table: "BorrowedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Users_MemberId",
                table: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_AdminProfileId",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "AdminProfileId",
                table: "BorrowedBooks");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "BorrowedBooks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowedBooks_MemberId",
                table: "BorrowedBooks",
                newName: "IX_BorrowedBooks_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Users_UserId",
                table: "BorrowedBooks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Users_UserId",
                table: "BorrowedBooks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BorrowedBooks",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowedBooks_UserId",
                table: "BorrowedBooks",
                newName: "IX_BorrowedBooks_MemberId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Users_MemberId",
                table: "BorrowedBooks",
                column: "MemberId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
