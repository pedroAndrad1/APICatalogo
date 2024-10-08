﻿// <auto-generated />
using System;
using APICatalogo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APICatalogo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240818225207_populate")]
    partial class populate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("APICatalogo.Domain.models.CategoriaModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created_at")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("Created_at"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0a79a13d-a78d-407f-be35-4e318a15e015"),
                            Created_at = new DateTime(2024, 8, 18, 19, 52, 7, 321, DateTimeKind.Local).AddTicks(7052),
                            ImageUrl = "bebidas.jpg",
                            Nome = "Bebidas"
                        },
                        new
                        {
                            Id = new Guid("2f7827a4-f815-4fc8-a359-a454a2fdcb89"),
                            Created_at = new DateTime(2024, 8, 18, 19, 52, 7, 321, DateTimeKind.Local).AddTicks(7065),
                            ImageUrl = "salgados.jpg",
                            Nome = "Salgados"
                        },
                        new
                        {
                            Id = new Guid("25a3cae5-b9cf-47f6-9b91-800a380ce5bf"),
                            Created_at = new DateTime(2024, 8, 18, 19, 52, 7, 321, DateTimeKind.Local).AddTicks(7066),
                            ImageUrl = "doces.jpg",
                            Nome = "Doces"
                        });
                });

            modelBuilder.Entity("APICatalogo.Domain.models.ProdutoModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created_at")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("Created_at"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<float>("Estoque")
                        .HasColumnType("float");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<int>("PrecoEmCentavos")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Produtos");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fee7c4c4-42d5-4995-ab86-19d1f4bc820e"),
                            CategoriaId = new Guid("0a79a13d-a78d-407f-be35-4e318a15e015"),
                            Created_at = new DateTime(2024, 8, 18, 19, 52, 7, 321, DateTimeKind.Local).AddTicks(7158),
                            Descricao = "da fruta",
                            Estoque = 100f,
                            ImageUrl = "suco_de_caju.jpg",
                            Nome = "Suco de caju",
                            PrecoEmCentavos = 800
                        },
                        new
                        {
                            Id = new Guid("80dbeb6e-c579-4380-bb34-998008543b90"),
                            CategoriaId = new Guid("2f7827a4-f815-4fc8-a359-a454a2fdcb89"),
                            Created_at = new DateTime(2024, 8, 18, 19, 52, 7, 321, DateTimeKind.Local).AddTicks(7160),
                            Descricao = "queijo e presunto",
                            Estoque = 100f,
                            ImageUrl = "joelho.jpg",
                            Nome = "Joelho",
                            PrecoEmCentavos = 600
                        },
                        new
                        {
                            Id = new Guid("9823f23d-31a1-4f5d-94fe-61aaf39a4bc1"),
                            CategoriaId = new Guid("25a3cae5-b9cf-47f6-9b91-800a380ce5bf"),
                            Created_at = new DateTime(2024, 8, 18, 19, 52, 7, 321, DateTimeKind.Local).AddTicks(7162),
                            Descricao = "de chocolate",
                            Estoque = 100f,
                            ImageUrl = "sorvete_de_chocolate.jpg",
                            Nome = "Sorvete",
                            PrecoEmCentavos = 1000
                        });
                });

            modelBuilder.Entity("APICatalogo.Domain.models.ProdutoModel", b =>
                {
                    b.HasOne("APICatalogo.Domain.models.CategoriaModel", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("APICatalogo.Domain.models.CategoriaModel", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
