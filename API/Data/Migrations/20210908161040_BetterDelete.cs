using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class BetterDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Users_AppUserId",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Categories_CategoryId",
                table: "Operations");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Users_AppUserId",
                table: "BankAccounts",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Categories_CategoryId",
                table: "Operations",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Users_AppUserId",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Categories_CategoryId",
                table: "Operations");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Users_AppUserId",
                table: "BankAccounts",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Categories_CategoryId",
                table: "Operations",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
