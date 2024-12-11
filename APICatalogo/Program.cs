using APICatalogo.CrossCutting.DependecyInjection;
using APICatalogo.Filters;
using APICatalogo.Infrastructure.Repositories;
using APICatalogo.Domain.Repositories;
using APICatalogo.Middlewares;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Asp.Versioning;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Asp.Versioning.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ApiLoggingFilter>();
        options.Filters.Add(typeof(ExceptionFilter));

    })
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Catalago", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Api Catalago", Version = "v2" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer JWT",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
          },
            new List<string>()
        }
    });
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,xmlFileName));
});
builder.Services.AddScoped<ApiLoggingFilter>();
// Add Logger
//builder.Logging.AddProvider(
//    new CustomLoggerProvider(
//        new CustomLoggerProviderConfiguration()
//        {
//            LogLevel = LogLevel.Information,
//        }
//    )
// );

// Add Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
    app.ConfigureExceptionHandler();
}
else
{
    app.UseExceptionHandler("/Error");
}
app.UseHttpsRedirection();

app.UseRateLimiter();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
