using APICatalogo.CrossCutting.DependecyInjection;
using APICatalogo.Filters;
using APICatalogo.Infrastructure.Repositories;
using APICatalogo.Domain.Repositories;
using APICatalogo.Middlewares;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;

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
});
// Add Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);
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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
