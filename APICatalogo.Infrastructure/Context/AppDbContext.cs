using APICatalogo.Domain.models
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        DbSet<ProdutoModel> Produtos { get; set; }
        DbSet<CategoriaModel> Categorias { get; set; }
    }
}
