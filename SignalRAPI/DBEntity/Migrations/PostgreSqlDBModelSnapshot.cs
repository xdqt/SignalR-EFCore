﻿// <auto-generated />
using System;
using DBEntity.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DBEntity.Migrations
{
    [DbContext(typeof(PostgreSqlDB))]
    partial class PostgreSqlDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DBEntity.Entities.Address", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("location")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("t_address");
                });

            modelBuilder.Entity("DBEntity.Entities.Student", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("addressid")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("addressid");

                    b.ToTable("t_student");
                });

            modelBuilder.Entity("DBEntity.Entities.Student", b =>
                {
                    b.HasOne("DBEntity.Entities.Address", "address")
                        .WithMany()
                        .HasForeignKey("addressid");

                    b.Navigation("address");
                });
#pragma warning restore 612, 618
        }
    }
}
