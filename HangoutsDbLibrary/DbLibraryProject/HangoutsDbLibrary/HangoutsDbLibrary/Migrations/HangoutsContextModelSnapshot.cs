﻿// <auto-generated />
using HangoutsDbLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HangoutsDbLibrary.Migrations
{
    [DbContext(typeof(HangoutsContext))]
    partial class HangoutsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HangoutsDbLibrary.Model.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BeginTimeActivity");

                    b.Property<string>("Description");

                    b.Property<string>("EndTimeActivity");

                    b.Property<int?>("LocationId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.GroupActivity", b =>
                {
                    b.Property<int>("ActivityId");

                    b.Property<int>("GroupId");

                    b.HasKey("ActivityId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupActivity");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.GroupAdmin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupAdminForeignKey");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GroupAdminForeignKey")
                        .IsUnique();

                    b.ToTable("GroupAdmin");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Interest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Interest");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adress");

                    b.Property<string>("City");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BeginnigTimePlan");

                    b.Property<string>("Description");

                    b.Property<string>("EndTimePlan");

                    b.Property<int?>("GroupId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Plan");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Fullname")
                        .IsRequired();

                    b.Property<string>("Password");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.UserGroup", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("GroupId");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroup");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Activity", b =>
                {
                    b.HasOne("HangoutsDbLibrary.Model.Location", "Location")
                        .WithMany("Activities")
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.GroupActivity", b =>
                {
                    b.HasOne("HangoutsDbLibrary.Model.Activity", "Activity")
                        .WithMany("GroupActivity")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HangoutsDbLibrary.Model.Group", "Group")
                        .WithMany("GroupActivity")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.GroupAdmin", b =>
                {
                    b.HasOne("HangoutsDbLibrary.Model.Group", "Group")
                        .WithOne("Admin")
                        .HasForeignKey("HangoutsDbLibrary.Model.GroupAdmin", "GroupAdminForeignKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Interest", b =>
                {
                    b.HasOne("HangoutsDbLibrary.Model.User", "User")
                        .WithMany("Interests")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Plan", b =>
                {
                    b.HasOne("HangoutsDbLibrary.Model.Group", "Group")
                        .WithMany("Plans")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.UserGroup", b =>
                {
                    b.HasOne("HangoutsDbLibrary.Model.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HangoutsDbLibrary.Model.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}