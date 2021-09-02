using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class Whyyyyyyyyy1214 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_OperationType_OperationTypeId1",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Users_AppUserId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_OperationTypeId1",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "OperationTypeId1",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Category");

            migrationBuilder.AlterColumn<int>(
                name: "ParentCategoryId",
                table: "Category",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "OperationTypeId",
                table: "Category",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Category",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Dupa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dupa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dupa_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_OperationTypeId",
                table: "Category",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Dupa_AppUserId",
                table: "Dupa",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_OperationType_OperationTypeId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Users_AppUserId",
                table: "Category");

            migrationBuilder.DropTable(
                name: "Dupa");

            migrationBuilder.DropIndex(
                name: "IX_Category_OperationTypeId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category");

            migrationBuilder.AlterColumn<int>(
                name: "ParentCategoryId",
                table: "Category",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OperationTypeId",
                table: "Category",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Category",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "OperationTypeId1",
                table: "Category",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Category",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Category_OperationTypeId1",
                table: "Category",
                column: "OperationTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_OperationType_OperationTypeId1",
                table: "Category",
                column: "OperationTypeId1",
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
        }
    }
}
