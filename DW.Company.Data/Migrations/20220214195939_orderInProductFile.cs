using Microsoft.EntityFrameworkCore.Migrations;

namespace DW.Company.Data.Migrations
{
    public partial class orderInProductFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "order_id",
                schema: "company",
                table: "product_file",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_id",
                schema: "company",
                table: "product_file");
        }
    }
}
