using Microsoft.EntityFrameworkCore.Migrations;

namespace DW.Company.Data.Migrations
{
    public partial class FileItemId_on_Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_file_item_FileItemId",
                schema: "company",
                table: "product");

            migrationBuilder.RenameColumn(
                name: "FileItemId",
                schema: "company",
                table: "product",
                newName: "file_item_id");

            migrationBuilder.RenameIndex(
                name: "IX_product_FileItemId",
                schema: "company",
                table: "product",
                newName: "IX_product_file_item_id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_file_item_file_item_id",
                schema: "company",
                table: "product",
                column: "file_item_id",
                principalSchema: "company",
                principalTable: "file_item",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_file_item_file_item_id",
                schema: "company",
                table: "product");

            migrationBuilder.RenameColumn(
                name: "file_item_id",
                schema: "company",
                table: "product",
                newName: "FileItemId");

            migrationBuilder.RenameIndex(
                name: "IX_product_file_item_id",
                schema: "company",
                table: "product",
                newName: "IX_product_FileItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_file_item_FileItemId",
                schema: "company",
                table: "product",
                column: "FileItemId",
                principalSchema: "company",
                principalTable: "file_item",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
