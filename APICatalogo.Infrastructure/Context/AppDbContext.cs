using APICatalogo.Domain.models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace APICatalogo.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
               Guid categoria0Id = Guid.NewGuid();
               Guid categoria1Id = Guid.NewGuid();
               Guid categoria2Id = Guid.NewGuid();

            modelBuilder.Entity<CategoriaModel>().HasData(
                new CategoriaModel { Id = categoria0Id, Nome = "Bebidas", ImageUrl = "bebidas.jpg", Created_at = DateTime.Now },
                new CategoriaModel { Id = categoria1Id, Nome = "Salgados", ImageUrl = "salgados.jpg", Created_at = DateTime.Now },
                new CategoriaModel { Id = categoria2Id, Nome = "Doces", ImageUrl = "doces.jpg", Created_at = DateTime.Now }
            );

            modelBuilder.Entity<ProdutoModel>().HasData(
                new ProdutoModel{ Id = Guid.NewGuid(),
                    CategoriaId = categoria0Id,
                    Nome = "Suco de caju",
                    Descricao = "da fruta",
                    PrecoEmCentavos = 8 * 100,
                    Estoque = 100,
                    ImageUrl = "suco_de_caju.jpg",
                    Created_at = DateTime.Now
                },
                 new ProdutoModel
                 {
                     Id = Guid.NewGuid(),
                     CategoriaId = categoria1Id,
                     Nome = "Joelho",
                     Descricao = "queijo e presunto",
                     PrecoEmCentavos = 6 * 100,
                     Estoque = 100,
                     ImageUrl = "joelho.jpg",
                     Created_at = DateTime.Now
                 },
                  new ProdutoModel
                  {
                      Id = Guid.NewGuid(),
                      CategoriaId = categoria2Id,
                      Nome = "Sorvete",
                      Descricao = "de chocolate",
                      PrecoEmCentavos = 10 * 100,
                      Estoque = 100,
                      ImageUrl = "sorvete_de_chocolate.jpg",
                      Created_at = DateTime.Now
                  }
            );
        }
    }
}
