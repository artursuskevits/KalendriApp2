﻿// <auto-generated />
using System;
using KalenderApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KalendriApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241028214528_AddTimezoneToUserAndEvent")]
    partial class AddTimezoneToUserAndEvent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KalenderApp.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "#FF0000",
                            Name = "Работа"
                        },
                        new
                        {
                            Id = 2,
                            Color = "#00FF00",
                            Name = "Личное"
                        },
                        new
                        {
                            Id = 3,
                            Color = "#0000FF",
                            Name = "Семья"
                        });
                });

            modelBuilder.Entity("KalenderApp.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Recurrence")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reminder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Timezone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Обсуждение проекта",
                            EndDateTime = new DateTime(2024, 10, 30, 1, 45, 26, 989, DateTimeKind.Local).AddTicks(3241),
                            Recurrence = "none",
                            Reminder = "email",
                            StartDateTime = new DateTime(2024, 10, 29, 23, 45, 26, 989, DateTimeKind.Local).AddTicks(3183),
                            Timezone = "Europe/Tallinn",
                            Title = "Встреча с командой",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "День рождения друга",
                            EndDateTime = new DateTime(2024, 10, 31, 2, 45, 26, 989, DateTimeKind.Local).AddTicks(3254),
                            Recurrence = "yearly",
                            Reminder = "notification",
                            StartDateTime = new DateTime(2024, 10, 30, 23, 45, 26, 989, DateTimeKind.Local).AddTicks(3251),
                            Timezone = "Europe/Tallinn",
                            Title = "День рождения",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("KalenderApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Timezone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@example.com",
                            Name = "Admin",
                            Password = "adminpassword",
                            Timezone = "Europe/Tallinn"
                        },
                        new
                        {
                            Id = 2,
                            Email = "user@example.com",
                            Name = "User",
                            Password = "userpassword",
                            Timezone = "Europe/Tallinn"
                        });
                });

            modelBuilder.Entity("KalenderApp.Models.Event", b =>
                {
                    b.HasOne("KalenderApp.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KalenderApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
