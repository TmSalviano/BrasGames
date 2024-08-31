﻿// <auto-generated />
using System;
using BrasGames.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BrasGames.Migrations
{
    [DbContext(typeof(BusinessDbContext))]
    [Migration("20240831020458_DayStatsKeyFromDateTimeToId")]
    partial class DayStatsKeyFromDateTimeToId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("BrasGames.Model.BusinessModels.DayStats", b =>
                {
                    b.Property<DateTime>("Day")
                        .HasColumnType("TEXT");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalConsumers")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalCost")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalProfit")
                        .HasColumnType("INTEGER");

                    b.HasKey("Day");

                    b.ToTable("Agenda");
                });

            modelBuilder.Entity("BrasGames.Model.BusinessModels.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndOfContract")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Salary")
                        .HasColumnType("REAL");

                    b.Property<int>("Sex")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearsWorked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("isFired")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
