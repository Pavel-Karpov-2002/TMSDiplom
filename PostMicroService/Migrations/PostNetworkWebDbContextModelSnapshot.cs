﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PostMicroService.DbStuff;

#nullable disable

namespace PostMicroService.Migrations
{
    [DbContext(typeof(PostNetworkWebDbContext))]
    partial class PostNetworkWebDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PostMicroService.DbStuff.Models.LocationPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LocationPosts");
                });

            modelBuilder.Entity("PostMicroService.DbStuff.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatorAvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatorUserId")
                        .HasColumnType("int");

                    b.Property<string>("CreatorUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateOfCreation")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LocationPostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationPostId")
                        .IsUnique()
                        .HasFilter("[LocationPostId] IS NOT NULL");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("PostMicroService.DbStuff.Models.Post", b =>
                {
                    b.HasOne("PostMicroService.DbStuff.Models.LocationPost", "LocationPost")
                        .WithOne("Post")
                        .HasForeignKey("PostMicroService.DbStuff.Models.Post", "LocationPostId");

                    b.Navigation("LocationPost");
                });

            modelBuilder.Entity("PostMicroService.DbStuff.Models.LocationPost", b =>
                {
                    b.Navigation("Post")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
