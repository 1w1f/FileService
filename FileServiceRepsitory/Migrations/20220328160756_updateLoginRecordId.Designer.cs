﻿// <auto-generated />
using System;
using FileServiceRepsitory.Repository.DbContextModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FileServiceRepsitory.Migrations
{
    [DbContext(typeof(FileServiceDbContext))]
    [Migration("20220328160756_updateLoginRecordId")]
    partial class updateLoginRecordId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DataModel.User.LoginRecordDto", b =>
                {
                    b.Property<int>("LoginRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LoginRecordId");

                    b.Property<string>("LoginIp")
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("LoginIp");

                    b.Property<string>("LoginTime")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("LoginTime");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("LoginRecordId");

                    b.HasIndex("UserId");

                    b.ToTable("LoginRecords");
                });

            modelBuilder.Entity("DataModel.User.UserDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("UserName");

                    b.Property<string>("PassWord")
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("DataModel.User.LoginRecordDto", b =>
                {
                    b.HasOne("DataModel.User.UserDto", null)
                        .WithMany("LoginRecords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataModel.User.UserDto", b =>
                {
                    b.Navigation("LoginRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
