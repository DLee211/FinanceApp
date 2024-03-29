using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Category_CategoryId",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CategoryId1",
                table: "Transaction",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Category_CategoryId",
                table: "Transaction",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Category_CategoryId1",
                table: "Transaction",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Category_CategoryId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Category_CategoryId1",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CategoryId1",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Transaction");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Category_CategoryId",
                table: "Transaction",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
