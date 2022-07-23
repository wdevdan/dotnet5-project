using Microsoft.EntityFrameworkCore.Migrations;

namespace DW.Company.Data.Migrations
{
    public partial class CategoryLeatherTypeIsRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leather_leather_type_leather_type_id",
                schema: "company",
                table: "leather");

            migrationBuilder.AlterColumn<int>(
                name: "leather_type_id",
                schema: "company",
                table: "leather",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_leather_leather_type_leather_type_id",
                schema: "company",
                table: "leather",
                column: "leather_type_id",
                principalSchema: "company",
                principalTable: "leather_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leather_leather_type_leather_type_id",
                schema: "company",
                table: "leather");

            migrationBuilder.AlterColumn<int>(
                name: "leather_type_id",
                schema: "company",
                table: "leather",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_leather_leather_type_leather_type_id",
                schema: "company",
                table: "leather",
                column: "leather_type_id",
                principalSchema: "company",
                principalTable: "leather_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
