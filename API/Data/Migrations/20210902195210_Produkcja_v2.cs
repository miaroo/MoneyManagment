using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class Produkcja_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_OperationType_OperationTypeId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Users_AppUserId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Operation_Category_CategoryId",
                table: "Operation");

            migrationBuilder.DropForeignKey(
                name: "FK_Operation_Saldo_SaldoId",
                table: "Operation");

            migrationBuilder.DropForeignKey(
                name: "FK_Saldo_Users_AppUserId",
                table: "Saldo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Saldo");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Saldo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_OperationType_OperationTypeId",
                table: "Category",
                column: "OperationTypeId",
                principalTable: "OperationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Users_AppUserId",
                table: "Category",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_Category_CategoryId",
                table: "Operation",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_Saldo_SaldoId",
                table: "Operation",
                column: "SaldoId",
                principalTable: "Saldo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Saldo_Users_AppUserId",
                table: "Saldo",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_OperationType_OperationTypeId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Users_AppUserId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Operation_Category_CategoryId",
                table: "Operation");

            migrationBuilder.DropForeignKey(
                name: "FK_Operation_Saldo_SaldoId",
                table: "Operation");

            migrationBuilder.DropForeignKey(
                name: "FK_Saldo_Users_AppUserId",
                table: "Saldo");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Saldo",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Saldo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_OperationType_OperationTypeId",
                table: "Category",
                column: "OperationTypeId",
                principalTable: "OperationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Users_AppUserId",
                table: "Category",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_Category_CategoryId",
                table: "Operation",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_Saldo_SaldoId",
                table: "Operation",
                column: "SaldoId",
                principalTable: "Saldo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Saldo_Users_AppUserId",
                table: "Saldo",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
