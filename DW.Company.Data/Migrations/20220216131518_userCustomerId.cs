using Microsoft.EntityFrameworkCore.Migrations;

namespace DW.Company.Data.Migrations
{
    public partial class userCustomerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "role",
                schema: "company",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "customer",
                oldClrType: typeof(string),
                oldType: "character varying(60)",
                oldMaxLength: 60,
                oldDefaultValue: "customer");

            migrationBuilder.AddColumn<int>(
                name: "customer_id",
                schema: "company",
                table: "user",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_customer_id",
                schema: "company",
                table: "user",
                column: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_customer_customer_id",
                schema: "company",
                table: "user",
                column: "customer_id",
                principalSchema: "company",
                principalTable: "customer",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_customer_customer_id",
                schema: "company",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_customer_id",
                schema: "company",
                table: "user");

            migrationBuilder.DropColumn(
                name: "customer_id",
                schema: "company",
                table: "user");

            migrationBuilder.AlterColumn<string>(
                name: "role",
                schema: "company",
                table: "user",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "customer",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "customer");
        }
    }
}
