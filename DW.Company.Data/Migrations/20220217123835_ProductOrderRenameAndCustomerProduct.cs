using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DW.Company.Data.Migrations
{
    public partial class ProductOrderRenameAndCustomerProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "order_id",
                schema: "company",
                table: "product_file",
                newName: "order");

            migrationBuilder.AddColumn<int>(
                name: "product_link_type",
                schema: "company",
                table: "customer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "customer_product",
                schema: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_customer_product_customer_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "company",
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customer_product_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "company",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customer_product_customer_id",
                schema: "company",
                table: "customer_product",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_product_product_id",
                schema: "company",
                table: "customer_product",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer_product",
                schema: "company");

            migrationBuilder.DropColumn(
                name: "product_link_type",
                schema: "company",
                table: "customer");

            migrationBuilder.RenameColumn(
                name: "order",
                schema: "company",
                table: "product_file",
                newName: "order_id");
        }
    }
}
