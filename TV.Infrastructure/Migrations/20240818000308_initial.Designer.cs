﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TV.Infrastructure;

#nullable disable

namespace TV.Infrastructure.Migrations
{
    [DbContext(typeof(TVDbContext))]
    [Migration("20240818000308_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TV.Domain.Models.Attachment", b =>
                {
                    b.Property<Guid>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TVShowId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AttachmentId");

                    b.HasIndex("TVShowId")
                        .IsUnique();

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("TV.Domain.Models.Languages", b =>
                {
                    b.Property<Guid>("LanguagesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LanguagesId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("TV.Domain.Models.LanguagesTVShow", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LanguagesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TVShowId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("LanguagesId");

                    b.HasIndex("TVShowId");

                    b.ToTable("LanguagesTVShows");
                });

            modelBuilder.Entity("TV.Domain.Models.TVShow", b =>
                {
                    b.Property<Guid>("TVShowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("bit");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<DateOnly>("RealeseDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TVShowId");

                    b.ToTable("TVShows");
                });

            modelBuilder.Entity("TV.Domain.Models.Attachment", b =>
                {
                    b.HasOne("TV.Domain.Models.TVShow", null)
                        .WithOne("Attachment")
                        .HasForeignKey("TV.Domain.Models.Attachment", "TVShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TV.Domain.Models.LanguagesTVShow", b =>
                {
                    b.HasOne("TV.Domain.Models.Languages", null)
                        .WithMany()
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TV.Domain.Models.TVShow", null)
                        .WithMany()
                        .HasForeignKey("TVShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TV.Domain.Models.TVShow", b =>
                {
                    b.Navigation("Attachment")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
