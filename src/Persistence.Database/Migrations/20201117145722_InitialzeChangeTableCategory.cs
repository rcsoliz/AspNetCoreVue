using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Database.Migrations
{
    public partial class InitialzeChangeTableCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Caterories_CateroryCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CateroryCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CateroryCategoryId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Caterories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Caterories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Caterories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CateroryCategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CateroryCategoryId",
                table: "Products",
                column: "CateroryCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Caterories_CateroryCategoryId",
                table: "Products",
                column: "CateroryCategoryId",
                principalTable: "Caterories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
