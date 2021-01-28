﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NykantAPI.Data;

namespace NykantAPI.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NykantAPI.Models.Bag", b =>
                {
                    b.Property<int>("BagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BagId");

                    b.ToTable("Bags");
                });

            modelBuilder.Entity("NykantAPI.Models.BagItem", b =>
                {
                    b.Property<int>("BagId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("BagId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("BagItems");
                });

            modelBuilder.Entity("NykantAPI.Models.CustomerInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CustomerInfos");
                });

            modelBuilder.Entity("NykantAPI.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ProductId = 1,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 2,
                            ProductId = 2,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 3,
                            ProductId = 3,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 4,
                            ProductId = 4,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 5,
                            ProductId = 5,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 6,
                            ProductId = 6,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 7,
                            ProductId = 7,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 8,
                            ProductId = 8,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 9,
                            ProductId = 9,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 10,
                            ProductId = 10,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 11,
                            ProductId = 11,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 12,
                            ProductId = 12,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 13,
                            ProductId = 13,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 14,
                            ProductId = 14,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 15,
                            ProductId = 15,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 16,
                            ProductId = 16,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 17,
                            ProductId = 17,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 18,
                            ProductId = 18,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 19,
                            ProductId = 19,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 20,
                            ProductId = 1,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 21,
                            ProductId = 2,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 22,
                            ProductId = 3,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 23,
                            ProductId = 4,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 24,
                            ProductId = 5,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 25,
                            ProductId = 6,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 26,
                            ProductId = 7,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 27,
                            ProductId = 8,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 28,
                            ProductId = 9,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 29,
                            ProductId = 10,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 30,
                            ProductId = 11,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 31,
                            ProductId = 12,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 32,
                            ProductId = 13,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 33,
                            ProductId = 14,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 34,
                            ProductId = 15,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 35,
                            ProductId = 16,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 36,
                            ProductId = 17,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 37,
                            ProductId = 18,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        },
                        new
                        {
                            Id = 38,
                            ProductId = 19,
                            Source = "../images/Finback-Chairs1-1280x853-c-default.jpg"
                        });
                });

            modelBuilder.Entity("NykantAPI.Models.Order", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("NykantAPI.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BagId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfWood")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BagId");

                    b.HasIndex("OrderUserId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "naturligt",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "stol",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 293, DateTimeKind.Local).AddTicks(1533),
                            Price = 1000,
                            Size = "5mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "valnød"
                        },
                        new
                        {
                            Id = 2,
                            Color = "farvet-overflade",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "stol",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3744),
                            Price = 1000,
                            Size = "10mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "eg"
                        },
                        new
                        {
                            Id = 3,
                            Color = "naturligt",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "bænk",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3825),
                            Price = 1000,
                            Size = "20mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "fyr"
                        },
                        new
                        {
                            Id = 4,
                            Color = "farvet-overflade",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "skærebræt",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3831),
                            Price = 1000,
                            Size = "5mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "eg"
                        },
                        new
                        {
                            Id = 5,
                            Color = "naturligt",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "bænk",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3834),
                            Price = 1000,
                            Size = "10mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "valnød"
                        },
                        new
                        {
                            Id = 6,
                            Color = "farvet-overflade",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "skærebræt",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3837),
                            Price = 1000,
                            Size = "20mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "fyr"
                        },
                        new
                        {
                            Id = 7,
                            Color = "naturligt",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "stol",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3840),
                            Price = 1000,
                            Size = "5mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "valnød"
                        },
                        new
                        {
                            Id = 8,
                            Color = "farvet-overflade",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "stol",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3844),
                            Price = 1000,
                            Size = "10mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "eg"
                        },
                        new
                        {
                            Id = 9,
                            Color = "naturligt",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "bænk",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3847),
                            Price = 1000,
                            Size = "20mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "fyr"
                        },
                        new
                        {
                            Id = 10,
                            Color = "farvet-overflade",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "skærebræt",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3850),
                            Price = 1000,
                            Size = "5mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "eg"
                        },
                        new
                        {
                            Id = 11,
                            Color = "naturligt",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "bænk",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3896),
                            Price = 1000,
                            Size = "10mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "valnød"
                        },
                        new
                        {
                            Id = 12,
                            Color = "farvet-overflade",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "skærebræt",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3900),
                            Price = 1000,
                            Size = "20mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "fyr"
                        },
                        new
                        {
                            Id = 13,
                            Color = "naturligt",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "stol",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3903),
                            Price = 1000,
                            Size = "5mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "valnød"
                        },
                        new
                        {
                            Id = 14,
                            Color = "farvet-overflade",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "stol",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3907),
                            Price = 1000,
                            Size = "10mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "eg"
                        },
                        new
                        {
                            Id = 15,
                            Color = "naturligt",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "bænk",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3910),
                            Price = 1000,
                            Size = "20mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "fyr"
                        },
                        new
                        {
                            Id = 16,
                            Color = "farvet-overflade",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "skærebræt",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3913),
                            Price = 1000,
                            Size = "5mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "eg"
                        },
                        new
                        {
                            Id = 17,
                            Color = "naturligt",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "bænk",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3916),
                            Price = 1000,
                            Size = "10mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "valnød"
                        },
                        new
                        {
                            Id = 18,
                            Color = "farvet-overflade",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "skærebræt",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3919),
                            Price = 1000,
                            Size = "20mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "fyr"
                        },
                        new
                        {
                            Id = 19,
                            Color = "naturligt",
                            Description = "a test object",
                            ImageSource = "../images/Finback-Chairs1-1280x853-c-default.jpg",
                            ItemType = "bænk",
                            LastModified = new DateTime(2021, 1, 28, 14, 22, 4, 295, DateTimeKind.Local).AddTicks(3923),
                            Price = 1000,
                            Size = "5mm",
                            Title = "Grøntsags Skærebræt",
                            TypeOfWood = "valnød"
                        });
                });

            modelBuilder.Entity("NykantAPI.Models.BagItem", b =>
                {
                    b.HasOne("NykantAPI.Models.Bag", "Bag")
                        .WithMany("BagItems")
                        .HasForeignKey("BagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NykantAPI.Models.Product", "Product")
                        .WithMany("BagItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NykantAPI.Models.Image", b =>
                {
                    b.HasOne("NykantAPI.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NykantAPI.Models.Product", b =>
                {
                    b.HasOne("NykantAPI.Models.Bag", null)
                        .WithMany("Products")
                        .HasForeignKey("BagId");

                    b.HasOne("NykantAPI.Models.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
