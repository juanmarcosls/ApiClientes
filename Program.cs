using ClientesAPI.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/api_log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog(); 

builder.Services.AddControllers();

builder.Services.AddDbContext<ClientesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClientesConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API de Clientes",
        Version = "v1",
        Description = "API para la gestión de clientes en Ficticia S.A.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Tu Nombre",
            Email = "tuemail@correo.com"
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Clientes v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();