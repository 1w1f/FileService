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
    [Migration("20230221031315_updateRecordWithConversion")]
    partial class updateRecordWithConversion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DataModel.User.LoginRecordDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("LoginRecordId");

                    b.Property<string>("LoginIp")
                        .HasColumnType("varchar(20)")
                        .HasColumnName("LoginIp");

                    b.Property<string>("LoginTime")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("LoginTime");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LoginRecords");
                });

            modelBuilder.Entity("DataModel.User.UserDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("UserId");

                    b.Property<string>("CreateTime")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("UserName");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UpdateTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("DataModel.User.LoginRecordDto", b =>
                {
                    b.HasOne("DataModel.User.UserDto", "User")
                        .WithMany("LoginRecords")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataModel.User.UserDto", b =>
                {
                    b.Navigation("LoginRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
