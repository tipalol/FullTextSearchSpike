﻿// <auto-generated />
using System;
using FullTextSearchSpike.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace FullTextSearchSpike.Migrations
{
    [DbContext(typeof(TextDbContext))]
    [Migration("20230313070925_Dev")]
    partial class Dev
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FullTextSearchSpike.Domain.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("name");

                    b.Property<NpgsqlTsVector>("SearchVector")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tsvector")
                        .HasColumnName("search_vector")
                        .HasAnnotation("Npgsql:TsVectorConfig", "russian")
                        .HasAnnotation("Npgsql:TsVectorProperties", new[] { "Name", "Content" });

                    b.HasKey("Id")
                        .HasName("pk_books");

                    b.HasIndex("SearchVector")
                        .HasDatabaseName("ix_books_search_vector");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("SearchVector"), "GIN");

                    b.ToTable("books", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}