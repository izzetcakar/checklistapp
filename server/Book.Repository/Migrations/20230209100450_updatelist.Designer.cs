﻿// <auto-generated />
using System;
using Book.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Book.Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230209100450_updatelist")]
    partial class updatelist
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Book.Core.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Book.Core.Models.Checklist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Checklists");
                });

            modelBuilder.Entity("Book.Core.Models.Consept", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Consepts");
                });

            modelBuilder.Entity("Book.Core.Models.Content", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("Book.Core.Models.ControlList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("ControlLists");
                });

            modelBuilder.Entity("Book.Core.Models.ListItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CheckListId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ConseptId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ContentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ControlListId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ItemScore")
                        .HasColumnType("integer");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<int?>("Relevance")
                        .HasColumnType("integer");

                    b.Property<int?>("Result")
                        .HasColumnType("integer");

                    b.Property<string>("Risk")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("SegmentId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Standard")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CheckListId");

                    b.HasIndex("ConseptId");

                    b.HasIndex("ContentId");

                    b.HasIndex("ControlListId");

                    b.HasIndex("SegmentId");

                    b.ToTable("ListItems");
                });

            modelBuilder.Entity("Book.Core.Models.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Book.Core.Models.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool?>("CanAdd")
                        .HasColumnType("boolean");

                    b.Property<bool?>("CanDelete")
                        .HasColumnType("boolean");

                    b.Property<bool?>("CanEdit")
                        .HasColumnType("boolean");

                    b.Property<bool?>("CanList")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Book.Core.Models.Segment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Segments");
                });

            modelBuilder.Entity("Book.Core.Models.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("CanAdd")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanEdit")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanList")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Book.Core.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Book.Core.Models.Checklist", b =>
                {
                    b.HasOne("Book.Core.Models.Organization", "Organization")
                        .WithMany("CheckLists")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Book.Core.Models.ListItem", b =>
                {
                    b.HasOne("Book.Core.Models.Category", "Category")
                        .WithMany("ListItems")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Book.Core.Models.Checklist", "Checklist")
                        .WithMany("ListItems")
                        .HasForeignKey("CheckListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Book.Core.Models.Consept", "Consept")
                        .WithMany("ListItems")
                        .HasForeignKey("ConseptId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Book.Core.Models.Content", "Content")
                        .WithMany("ListItems")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Book.Core.Models.ControlList", "ControlList")
                        .WithMany("ListItems")
                        .HasForeignKey("ControlListId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Book.Core.Models.Segment", "Segment")
                        .WithMany("ListItems")
                        .HasForeignKey("SegmentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");

                    b.Navigation("Checklist");

                    b.Navigation("Consept");

                    b.Navigation("Content");

                    b.Navigation("ControlList");

                    b.Navigation("Segment");
                });

            modelBuilder.Entity("Book.Core.Models.Permission", b =>
                {
                    b.HasOne("Book.Core.Models.User", "User")
                        .WithMany("Permissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Book.Core.Models.Subscription", b =>
                {
                    b.HasOne("Book.Core.Models.Organization", "Organization")
                        .WithMany("Subs")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Book.Core.Models.User", "User")
                        .WithMany("Subs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Book.Core.Models.Category", b =>
                {
                    b.Navigation("ListItems");
                });

            modelBuilder.Entity("Book.Core.Models.Checklist", b =>
                {
                    b.Navigation("ListItems");
                });

            modelBuilder.Entity("Book.Core.Models.Consept", b =>
                {
                    b.Navigation("ListItems");
                });

            modelBuilder.Entity("Book.Core.Models.Content", b =>
                {
                    b.Navigation("ListItems");
                });

            modelBuilder.Entity("Book.Core.Models.ControlList", b =>
                {
                    b.Navigation("ListItems");
                });

            modelBuilder.Entity("Book.Core.Models.Organization", b =>
                {
                    b.Navigation("CheckLists");

                    b.Navigation("Subs");
                });

            modelBuilder.Entity("Book.Core.Models.Segment", b =>
                {
                    b.Navigation("ListItems");
                });

            modelBuilder.Entity("Book.Core.Models.User", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("Subs");
                });
#pragma warning restore 612, 618
        }
    }
}
