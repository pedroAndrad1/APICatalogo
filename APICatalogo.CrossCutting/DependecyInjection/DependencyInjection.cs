﻿using APICatalogo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using APICatalogo.Application.Queries.Produtos;
using APICatalogo.Domain.Repositories;
using APICatalogo.Infrastructure.Repositories;
using APICatalogo.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            // IDENTITY
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            // JWT TOKEN
            var secretKey = configuration["JWT:SecretKey"] ?? throw new ArgumentException("Chave secrete JWT não encontrada!");
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });
            services.AddAuthorization();
            services.AddAuthentication("Bearer").AddJwtBearer();
            // REPOSITORIES
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // UNIT OF WORK
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            // MEDIATOR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof (GetProdutosQuery)));
            // AUTO MAPPER
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
           
        }
    }
}
