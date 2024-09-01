using APICatalogo.CrossCutting.DependecyInjection;
using APICatalogo.Filters;
using APICatalogo.Infrastructure.Repositories;
using APICatalogo.Domain.Repositories;
using APICatalogo.Middlewares;
using System.Text.Json.Serialization;

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
builder.Services.AddSwaggerGen();
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
