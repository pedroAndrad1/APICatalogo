using APICatalogo.Infrastructure.Context;
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
using APICatalogo.Domain.Services;
using APICatalogo.Application.Services;
using APICatalogo.Domain.Identity;
using APICatalogo.CrossCutting.Options;
using Microsoft.AspNetCore.Builder;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Http;

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
            services.AddIdentity<ApplicationUser, IdentityRole>()
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
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("SuperAdmin", policy => policy.RequireRole("Admin").RequireClaim("Id","SuperAdmin"));
                options.AddPolicy("User", policy => policy.RequireRole("User"));

                options.AddPolicy("Exclusive", policy =>
                {
                    policy.RequireAssertion(context =>
                        context.User.HasClaim( claim =>
                            claim.Type == "Id" && claim.Value == "SuperAdmin"
                        ) ||
                        context.User.IsInRole("SuperAdmin")
                    );
                });
            });
            // services.AddAuthentication("Bearer").AddJwtBearer();
            services.AddScoped<ITokenService, TokenService>();
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
            // CORS
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                    policy.WithOrigins("METODO_DOMINIO_E_PORTA_DO_CLIENTE_AQUI")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                );
            });
            // Rate Limit
            var rateLimitOptions = new RateLimitOptions();
            configuration.GetSection(RateLimitOptions.RateLimitOptionsSection).Bind(rateLimitOptions);
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.GlobalLimiter =
                    PartitionedRateLimiter.Create<HttpContext, string>(
                        httpContext => RateLimitPartition.GetFixedWindowLimiter(
                            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                            factory: partition => new FixedWindowRateLimiterOptions
                            {
                                AutoReplenishment = rateLimitOptions.AutoReplenishment,
                                PermitLimit = rateLimitOptions.PermitLimit,
                                QueueLimit = rateLimitOptions.QueueLimit,
                                Window = TimeSpan.FromSeconds(rateLimitOptions.Window)   
                            }
                        )
                    );
            });

            return services;
           
        }
    }
}
