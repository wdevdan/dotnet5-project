﻿// <auto-generated />
using System;
using DW.Company.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DW.Company.Data.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20220210121401_CategoryDescriptionIsNotRequired")]
    partial class CategoryDescriptionIsNotRequired
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("company")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DW.Company.Entities.Entity.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("category");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("alias");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedAt")
                        .HasMaxLength(50)
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("document");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasMaxLength(50)
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated");

                    b.HasKey("Id");

                    b.ToTable("customer");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.Designer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasMaxLength(50)
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<int?>("FileItemId")
                        .HasColumnType("integer")
                        .HasColumnName("file_item_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("surname");

                    b.Property<DateTime>("UpdatedAt")
                        .HasMaxLength(50)
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated");

                    b.HasKey("Id");

                    b.HasIndex("FileItemId");

                    b.ToTable("designer");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.FileItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<string>("CurrentName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("current_name");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("extension");

                    b.Property<string>("Md5")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("character varying(36)")
                        .HasColumnName("md5");

                    b.Property<string>("OriginName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("origin_name");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)")
                        .HasColumnName("path");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated");

                    b.HasKey("Id");

                    b.ToTable("file_item");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.ItemVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("version");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.Leather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("code");

                    b.Property<int?>("FileItemId")
                        .HasColumnType("integer")
                        .HasColumnName("file_item_id");

                    b.Property<int?>("LeatherTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("leather_type_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("FileItemId");

                    b.HasIndex("LeatherTypeId");

                    b.ToTable("leather");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.LeatherType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("leather_type");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int?>("DesignerId")
                        .HasColumnType("integer")
                        .HasColumnName("designer_id");

                    b.Property<int?>("FileItemId")
                        .HasColumnType("integer")
                        .HasColumnName("file_item_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated");

                    b.HasKey("Id");

                    b.HasIndex("DesignerId");

                    b.HasIndex("FileItemId");

                    b.ToTable("product");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("product_category");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.ProductFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FileItemId")
                        .HasColumnType("integer")
                        .HasColumnName("file_item_id");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.HasKey("Id");

                    b.HasIndex("FileItemId");

                    b.HasIndex("ProductId");

                    b.ToTable("product_file");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("firstname");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("lastname");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasDefaultValue("customer")
                        .HasColumnName("role");

                    b.Property<DateTime>("ValidSince")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("valid_since");

                    b.Property<DateTime>("ValidUntil")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("valid_until");

                    b.HasKey("UserId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.Designer", b =>
                {
                    b.HasOne("DW.Company.Entities.Entity.FileItem", "FileItem")
                        .WithMany()
                        .HasForeignKey("FileItemId");

                    b.Navigation("FileItem");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.Leather", b =>
                {
                    b.HasOne("DW.Company.Entities.Entity.FileItem", "FileItem")
                        .WithMany()
                        .HasForeignKey("FileItemId");

                    b.HasOne("DW.Company.Entities.Entity.LeatherType", "LeatherType")
                        .WithMany("Leathers")
                        .HasForeignKey("LeatherTypeId");

                    b.Navigation("FileItem");

                    b.Navigation("LeatherType");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.Product", b =>
                {
                    b.HasOne("DW.Company.Entities.Entity.Designer", "Designer")
                        .WithMany()
                        .HasForeignKey("DesignerId");

                    b.HasOne("DW.Company.Entities.Entity.FileItem", "FileItem")
                        .WithMany()
                        .HasForeignKey("FileItemId");

                    b.Navigation("Designer");

                    b.Navigation("FileItem");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.ProductCategory", b =>
                {
                    b.HasOne("DW.Company.Entities.Entity.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DW.Company.Entities.Entity.Product", "Product")
                        .WithMany("Categories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.ProductFile", b =>
                {
                    b.HasOne("DW.Company.Entities.Entity.FileItem", "FileItem")
                        .WithMany()
                        .HasForeignKey("FileItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DW.Company.Entities.Entity.Product", "Product")
                        .WithMany("Files")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FileItem");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.LeatherType", b =>
                {
                    b.Navigation("Leathers");
                });

            modelBuilder.Entity("DW.Company.Entities.Entity.Product", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
