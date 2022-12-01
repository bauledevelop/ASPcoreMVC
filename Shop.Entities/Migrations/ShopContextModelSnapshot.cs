﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Entities.Enities;

#nullable disable

namespace Shop.Entities.Migrations
{
    [DbContext(typeof(ShopContext))]
    partial class ShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Shop.Entities.Enities.Account", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"), 1L, 1);

                    b.Property<string>("Email")
                        .HasMaxLength(2048)
                        .HasColumnType("varchar(2048)");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("ID", "Email", "Username");

                    b.ToTable("Accounts", (string)null);

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            Email = "abc",
                            Username = "admin",
                            AccountType = 1,
                            Address = "a",
                            BirthDay = new DateTime(2022, 11, 27, 1, 10, 12, 885, DateTimeKind.Local).AddTicks(7186),
                            CreatedDate = new DateTime(2022, 11, 27, 1, 10, 12, 885, DateTimeKind.Local).AddTicks(7176),
                            IsActive = true,
                            IsDelete = false,
                            Name = "Dương",
                            Password = "1",
                            Phone = "123",
                            Sex = 1,
                            Status = true
                        },
                        new
                        {
                            ID = 2L,
                            Email = "zxxz",
                            Username = "user",
                            AccountType = 2,
                            Address = "a",
                            BirthDay = new DateTime(2022, 11, 27, 1, 10, 12, 885, DateTimeKind.Local).AddTicks(7190),
                            CreatedDate = new DateTime(2022, 11, 27, 1, 10, 12, 885, DateTimeKind.Local).AddTicks(7189),
                            IsActive = true,
                            IsDelete = false,
                            Name = "Dương",
                            Password = "1",
                            Phone = "123",
                            Sex = 2,
                            Status = true
                        });
                });

            modelBuilder.Entity("Shop.Entities.Enities.CategoryProduct", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"), 1L, 1);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("IDMenu")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("IDMenu");

                    b.ToTable("CategoryProducts", (string)null);
                });

            modelBuilder.Entity("Shop.Entities.Enities.Comment", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"), 1L, 1);

                    b.Property<string>("Content")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("IDAccount")
                        .HasColumnType("bigint");

                    b.Property<long>("IDProduct")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("IDAccount");

                    b.HasIndex("IDProduct");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("Shop.Entities.Enities.File", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"), 1L, 1);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileContent")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<long>("IDProduct")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("IDProduct");

                    b.ToTable("Files", (string)null);
                });

            modelBuilder.Entity("Shop.Entities.Enities.Menu", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"), 1L, 1);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Menus", (string)null);
                });

            modelBuilder.Entity("Shop.Entities.Enities.Order", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("IDAccount")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<long>("Total")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("IDAccount");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Shop.Entities.Enities.OrderDetail", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"), 1L, 1);

                    b.Property<int>("Color")
                        .HasColumnType("int");

                    b.Property<long>("IDOrder")
                        .HasColumnType("bigint");

                    b.Property<long>("IDProduct")
                        .HasColumnType("bigint");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<long>("Total")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("IDOrder");

                    b.HasIndex("IDProduct");

                    b.ToTable("OrderDetails", (string)null);
                });

            modelBuilder.Entity("Shop.Entities.Enities.Payment", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<long>("IDAccount")
                        .HasColumnType("bigint");

                    b.Property<long>("IDOrder")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("IDAccount");

                    b.HasIndex("IDOrder");

                    b.ToTable("Payments", (string)null);
                });

            modelBuilder.Entity("Shop.Entities.Enities.Product", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"), 1L, 1);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("Detail")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<long>("IDCategoryProduct")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("IDCategoryProduct");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Shop.Entities.Enities.Rate", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"), 1L, 1);

                    b.Property<long>("IDAccount")
                        .HasColumnType("bigint");

                    b.Property<long>("IDProduct")
                        .HasColumnType("bigint");

                    b.Property<int>("rate")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IDAccount");

                    b.HasIndex("IDProduct");

                    b.ToTable("Rates", (string)null);
                });

            modelBuilder.Entity("Shop.Entities.Enities.CategoryProduct", b =>
                {
                    b.HasOne("Shop.Entities.Enities.Account", "Account")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("CreatedBy")
                        .HasPrincipalKey("ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shop.Entities.Enities.Menu", "Menu")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("IDMenu")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("Shop.Entities.Enities.Comment", b =>
                {
                    b.HasOne("Shop.Entities.Enities.Account", "Account")
                        .WithMany("Comments")
                        .HasForeignKey("IDAccount")
                        .HasPrincipalKey("ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shop.Entities.Enities.Product", "Product")
                        .WithMany("Comments")
                        .HasForeignKey("IDProduct")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop.Entities.Enities.File", b =>
                {
                    b.HasOne("Shop.Entities.Enities.Account", "Account")
                        .WithMany("Files")
                        .HasForeignKey("CreatedBy")
                        .HasPrincipalKey("ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shop.Entities.Enities.Product", "Product")
                        .WithMany("Files")
                        .HasForeignKey("IDProduct")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop.Entities.Enities.Menu", b =>
                {
                    b.HasOne("Shop.Entities.Enities.Account", "Account")
                        .WithMany("Menus")
                        .HasForeignKey("CreatedBy")
                        .HasPrincipalKey("ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Shop.Entities.Enities.Order", b =>
                {
                    b.HasOne("Shop.Entities.Enities.Account", "Account")
                        .WithMany("Orders")
                        .HasForeignKey("IDAccount")
                        .HasPrincipalKey("ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Shop.Entities.Enities.OrderDetail", b =>
                {
                    b.HasOne("Shop.Entities.Enities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("IDOrder")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shop.Entities.Enities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("IDProduct")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop.Entities.Enities.Payment", b =>
                {
                    b.HasOne("Shop.Entities.Enities.Account", "Account")
                        .WithMany("Payments")
                        .HasForeignKey("IDAccount")
                        .HasPrincipalKey("ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shop.Entities.Enities.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("IDOrder")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Shop.Entities.Enities.Product", b =>
                {
                    b.HasOne("Shop.Entities.Enities.Account", "Account")
                        .WithMany("Products")
                        .HasForeignKey("CreatedBy")
                        .HasPrincipalKey("ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shop.Entities.Enities.CategoryProduct", "CategoryProduct")
                        .WithMany("Products")
                        .HasForeignKey("IDCategoryProduct")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("CategoryProduct");
                });

            modelBuilder.Entity("Shop.Entities.Enities.Rate", b =>
                {
                    b.HasOne("Shop.Entities.Enities.Account", "Account")
                        .WithMany("Rates")
                        .HasForeignKey("IDAccount")
                        .HasPrincipalKey("ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shop.Entities.Enities.Product", "Product")
                        .WithMany("Rates")
                        .HasForeignKey("IDProduct")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop.Entities.Enities.Account", b =>
                {
                    b.Navigation("CategoryProducts");

                    b.Navigation("Comments");

                    b.Navigation("Files");

                    b.Navigation("Menus");

                    b.Navigation("Orders");

                    b.Navigation("Payments");

                    b.Navigation("Products");

                    b.Navigation("Rates");
                });

            modelBuilder.Entity("Shop.Entities.Enities.CategoryProduct", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Shop.Entities.Enities.Menu", b =>
                {
                    b.Navigation("CategoryProducts");
                });

            modelBuilder.Entity("Shop.Entities.Enities.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Shop.Entities.Enities.Product", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Files");

                    b.Navigation("OrderDetails");

                    b.Navigation("Rates");
                });
#pragma warning restore 612, 618
        }
    }
}
