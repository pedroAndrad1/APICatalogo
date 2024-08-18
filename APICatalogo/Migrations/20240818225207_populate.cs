using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class populate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "ImageUrl", "Nome" },
                values: new object[,]
                {
                    { new Guid("0a79a13d-a78d-407f-be35-4e318a15e015"), "bebidas.jpg", "Bebidas" },
                    { new Guid("25a3cae5-b9cf-47f6-9b91-800a380ce5bf"), "doces.jpg", "Doces" },
                    { new Guid("2f7827a4-f815-4fc8-a359-a454a2fdcb89"), "salgados.jpg", "Salgados" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "CategoriaId", "Descricao", "Estoque", "ImageUrl", "Nome", "PrecoEmCentavos" },
                values: new object[,]
                {
                    { new Guid("80dbeb6e-c579-4380-bb34-998008543b90"), new Guid("2f7827a4-f815-4fc8-a359-a454a2fdcb89"), "queijo e presunto", 100f, "joelho.jpg", "Joelho", 600 },
                    { new Guid("9823f23d-31a1-4f5d-94fe-61aaf39a4bc1"), new Guid("25a3cae5-b9cf-47f6-9b91-800a380ce5bf"), "de chocolate", 100f, "sorvete_de_chocolate.jpg", "Sorvete", 1000 },
                    { new Guid("fee7c4c4-42d5-4995-ab86-19d1f4bc820e"), new Guid("0a79a13d-a78d-407f-be35-4e318a15e015"), "da fruta", 100f, "suco_de_caju.jpg", "Suco de caju", 800 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("80dbeb6e-c579-4380-bb34-998008543b90"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("9823f23d-31a1-4f5d-94fe-61aaf39a4bc1"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("fee7c4c4-42d5-4995-ab86-19d1f4bc820e"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("0a79a13d-a78d-407f-be35-4e318a15e015"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("25a3cae5-b9cf-47f6-9b91-800a380ce5bf"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("2f7827a4-f815-4fc8-a359-a454a2fdcb89"));
        }
    }
}
