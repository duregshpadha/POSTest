﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using POS.DAL;

namespace POS.DAL.Migrations
{
    [DbContext(typeof(POSDBContext))]
    partial class POSDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("POS.DAL.Models.MasterCustomer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(42)")
                        .HasMaxLength(42);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("MasterCustomer","dbo");

                    b.HasData(
                        new
                        {
                            Id = "09052020-074005322-f7390afc-c276-415f-9a20",
                            Name = "Ram",
                            Phone = "1234567890"
                        });
                });

            modelBuilder.Entity("POS.DAL.Models.MasterItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(42)")
                        .HasMaxLength(42);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MasterItem","dbo");

                    b.HasData(
                        new
                        {
                            Id = "09052020-073914277-53924f75-cae5-475b-a5eb",
                            Name = "Rice",
                            Rate = 10m,
                            Stock = 10
                        });
                });

            modelBuilder.Entity("POS.DAL.Models.POSDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(42)")
                        .HasMaxLength(42);

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(42)")
                        .HasMaxLength(42);

                    b.Property<int>("ItemQuantity")
                        .HasColumnType("int");

                    b.Property<decimal>("ItemRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("POSMainID")
                        .IsRequired()
                        .HasColumnType("nvarchar(42)")
                        .HasMaxLength(42);

                    b.Property<string>("SaleOrReturn")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("POSMainID");

                    b.ToTable("POSDetail","dbo");

                    b.HasData(
                        new
                        {
                            Id = "09052020-073953202-81be9799-750d-4955-bb17",
                            ItemId = "09052020-073914277-53924f75-cae5-475b-a5eb",
                            ItemQuantity = 1,
                            ItemRate = 10m,
                            POSMainID = "09052020-074037271-6be8d386-4ad5-4e92-b827",
                            SaleOrReturn = "S",
                            TotalAmount = 10m
                        });
                });

            modelBuilder.Entity("POS.DAL.Models.POSMain", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(42)")
                        .HasMaxLength(42);

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(42)")
                        .HasMaxLength(42);

                    b.Property<DateTime>("PosDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TotalQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("POSMain","dbo");

                    b.HasData(
                        new
                        {
                            Id = "09052020-074037271-6be8d386-4ad5-4e92-b827",
                            CustomerId = "09052020-074005322-f7390afc-c276-415f-9a20",
                            PosDate = new DateTime(2020, 9, 5, 13, 28, 1, 921, DateTimeKind.Local).AddTicks(2564),
                            TotalAmount = 10m,
                            TotalQuantity = 1
                        });
                });

            modelBuilder.Entity("POS.DAL.Models.POSDetail", b =>
                {
                    b.HasOne("POS.DAL.Models.MasterItem", "MasterItem")
                        .WithMany("POSDetails")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POS.DAL.Models.POSMain", "POSMain")
                        .WithMany("POSDetails")
                        .HasForeignKey("POSMainID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("POS.DAL.Models.POSMain", b =>
                {
                    b.HasOne("POS.DAL.Models.MasterCustomer", "MasterCustomer")
                        .WithMany("POSMains")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
