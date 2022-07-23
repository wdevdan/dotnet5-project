using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace DW.Company.Data.Migrations
{
    public partial class Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "company");

            migrationBuilder.CreateTable(
                name: "category",
                schema: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                schema: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    alias = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    document = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", maxLength: 50, nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "file_item",
                schema: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    extension = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    origin_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    current_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    path = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    md5 = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_item", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "leather_type",
                schema: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leather_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    lastname = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    login = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    email = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    password = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    role = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false, defaultValue: "customer"),
                    valid_since = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    valid_until = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "version",
                schema: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_version", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "designer",
                schema: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    file_item_id = table.Column<int>(type: "integer", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", maxLength: 50, nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_designer", x => x.id);
                    table.ForeignKey(
                        name: "FK_designer_file_item_file_item_id",
                        column: x => x.file_item_id,
                        principalSchema: "company",
                        principalTable: "file_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "leather",
                schema: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    file_item_id = table.Column<int>(type: "integer", nullable: true),
                    leather_type_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leather", x => x.id);
                    table.ForeignKey(
                        name: "FK_leather_file_item_file_item_id",
                        column: x => x.file_item_id,
                        principalSchema: "company",
                        principalTable: "file_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_leather_leather_type_leather_type_id",
                        column: x => x.leather_type_id,
                        principalSchema: "company",
                        principalTable: "leather_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    designer_id = table.Column<int>(type: "integer", nullable: true),
                    FileItemId = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_designer_designer_id",
                        column: x => x.designer_id,
                        principalSchema: "company",
                        principalTable: "designer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_product_file_item_FileItemId",
                        column: x => x.FileItemId,
                        principalSchema: "company",
                        principalTable: "file_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_category",
                schema: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_category", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_category_category_category_id",
                        column: x => x.category_id,
                        principalSchema: "company",
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_category_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "company",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_file",
                schema: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    file_item_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_file", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_file_file_item_file_item_id",
                        column: x => x.file_item_id,
                        principalSchema: "company",
                        principalTable: "file_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_file_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "company",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_designer_file_item_id",
                schema: "company",
                table: "designer",
                column: "file_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_leather_file_item_id",
                schema: "company",
                table: "leather",
                column: "file_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_leather_leather_type_id",
                schema: "company",
                table: "leather",
                column: "leather_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_designer_id",
                schema: "company",
                table: "product",
                column: "designer_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_FileItemId",
                schema: "company",
                table: "product",
                column: "FileItemId");

            migrationBuilder.CreateIndex(
                name: "IX_product_category_category_id",
                schema: "company",
                table: "product_category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_category_product_id",
                schema: "company",
                table: "product_category",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_file_file_item_id",
                schema: "company",
                table: "product_file",
                column: "file_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_file_product_id",
                schema: "company",
                table: "product_file",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer",
                schema: "company");

            migrationBuilder.DropTable(
                name: "leather",
                schema: "company");

            migrationBuilder.DropTable(
                name: "product_category",
                schema: "company");

            migrationBuilder.DropTable(
                name: "product_file",
                schema: "company");

            migrationBuilder.DropTable(
                name: "user",
                schema: "company");

            migrationBuilder.DropTable(
                name: "version",
                schema: "company");

            migrationBuilder.DropTable(
                name: "leather_type",
                schema: "company");

            migrationBuilder.DropTable(
                name: "category",
                schema: "company");

            migrationBuilder.DropTable(
                name: "product",
                schema: "company");

            migrationBuilder.DropTable(
                name: "designer",
                schema: "company");

            migrationBuilder.DropTable(
                name: "file_item",
                schema: "company");
        }
    }
}
