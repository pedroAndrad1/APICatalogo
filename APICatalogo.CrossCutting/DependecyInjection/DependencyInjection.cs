using APICatalogo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using APICatalogo.Application.Queries.Produtos;
using APICatalogo.Infrastructure.Repositories.Abstractions;
using APICatalogo.Infrastructure.Repositories;

namespace APICatalogo.CrossCutting.DependecyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            // DB CONTEXT
            var mySqlConnection = configuration.GetConnectionString("Default");
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(mySqlConnection,
                ServerVersion.AutoDetect(mySqlConnection),
                b => b.MigrationsAssembly("APICatalogo"))
            );
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // UNIT OF WORK
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // MEDIATOR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof (GetProdutosQuery)));
            return services;
           
        }
    }
}
