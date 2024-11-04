using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class alterIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("3ea0c6ec-0a72-4507-be09-22a14e2579f3"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("c50c913a-4814-4883-abdf-76248165a575"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("f47fa3d8-474f-49cc-97cd-3eaa67aeee38"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("afa6a6ce-7412-4ba3-ad37-397725fb7a98"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("ba10dd56-a519-4852-bd1d-3032d9da1dd3"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("d1987865-5cda-4c57-b2d2-9dd4edd82edd"));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "ImageUrl", "Nome" },
                values: new object[,]
                {
                    { new Guid("5bdb4c1f-ac28-43af-8f86-ef774808c287"), "doces.jpg", "Doces" },
                    { new Guid("5c895696-b153-4dbc-bf73-5db7d4446934"), "salgados.jpg", "Salgados" },
                    { new Guid("fa0bc167-3305-4074-ba5f-76b76ffa5d9c"), "bebidas.jpg", "Bebidas" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "CategoriaId", "Descricao", "Estoque", "ImageUrl", "Nome", "PrecoEmCentavos" },
                values: new object[,]
                {
                    { new Guid("029822f0-549c-4653-9c9b-92a18fa4743e"), new Guid("5c895696-b153-4dbc-bf73-5db7d4446934"), "queijo e presunto", 100f, "joelho.jpg", "Joelho", 600 },
                    { new Guid("8288516b-ef3b-4f23-8b94-147bcd2538c2"), new Guid("fa0bc167-3305-4074-ba5f-76b76ffa5d9c"), "da fruta", 100f, "suco_de_caju.jpg", "Suco de caju", 800 },
                    { new Guid("e7777aed-1cf4-406e-8586-8a4fcd2fd0d8"), new Guid("5bdb4c1f-ac28-43af-8f86-ef774808c287"), "de chocolate", 100f, "sorvete_de_chocolate.jpg", "Sorvete", 1000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("029822f0-549c-4653-9c9b-92a18fa4743e"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("8288516b-ef3b-4f23-8b94-147bcd2538c2"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("e7777aed-1cf4-406e-8586-8a4fcd2fd0d8"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("5bdb4c1f-ac28-43af-8f86-ef774808c287"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("5c895696-b153-4dbc-bf73-5db7d4446934"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("fa0bc167-3305-4074-ba5f-76b76ffa5d9c"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "ImageUrl", "Nome" },
                values: new object[,]
                {
                    { new Guid("afa6a6ce-7412-4ba3-ad37-397725fb7a98"), "salgados.jpg", "Salgados" },
                    { new Guid("ba10dd56-a519-4852-bd1d-3032d9da1dd3"), "doces.jpg", "Doces" },
                    { new Guid("d1987865-5cda-4c57-b2d2-9dd4edd82edd"), "bebidas.jpg", "Bebidas" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "CategoriaId", "Descricao", "Estoque", "ImageUrl", "Nome", "PrecoEmCentavos" },
                values: new object[,]
                {
                    { new Guid("3ea0c6ec-0a72-4507-be09-22a14e2579f3"), new Guid("afa6a6ce-7412-4ba3-ad37-397725fb7a98"), "queijo e presunto", 100f, "joelho.jpg", "Joelho", 600 },
                    { new Guid("c50c913a-4814-4883-abdf-76248165a575"), new Guid("d1987865-5cda-4c57-b2d2-9dd4edd82edd"), "da fruta", 100f, "suco_de_caju.jpg", "Suco de caju", 800 },
                    { new Guid("f47fa3d8-474f-49cc-97cd-3eaa67aeee38"), new Guid("ba10dd56-a519-4852-bd1d-3032d9da1dd3"), "de chocolate", 100f, "sorvete_de_chocolate.jpg", "Sorvete", 1000 }
                });
        }
    }
}
