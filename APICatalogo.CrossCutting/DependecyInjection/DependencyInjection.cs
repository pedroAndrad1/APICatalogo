using APICatalogo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using APICatalogo.Application.Queries.Produtos;

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
            ); ;
            // MEDIATOR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof (GetProdutosQuery)));

            return services;
           
        }
    }
}
