using HostingProcessEquipmentService.Application.Contracts;
using HostingProcessEquipmentService.Application.Services;
using HostingProcessEquipmentService.Infrastructure.Context;
using HostingProcessEquipmentService.Infrastructure.Contracts;
using HostingProcessEquipmentService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HostingProcessEquipmentService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        // Register repositories
        builder.Services.AddScoped<IContractRepository, ContractRepository>();

        // Register services
        builder.Services.AddScoped<IContractService, ContractService>();

        // Add controllers
        builder.Services.AddControllers();
        // Add services to the container.

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.MapControllers();
        app.UseAuthorization();

        app.Run();
    }
    
    /*var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    // Configure database connection
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Register repositories
    builder.Services.AddScoped<IContractRepository, ContractRepository>();

    // Register services
    builder.Services.AddScoped<IContractService, ContractService>();

    // Add controllers
    builder.Services.AddControllers();

    // Add Swagger/OpenAPI support
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Equipment Placement API",
            Version = "v1",
            Description = "API for managing equipment placement contracts"
        });
    });

// Configure API key authentication middleware (simple example)
/*builder.Services.AddAuthentication("ApiKey")
    .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>("ApiKey", options => { });#1#

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(/*c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Equipment Placement API v1");
        c.RoutePrefix = string.Empty; // Make Swagger UI accessible at root
    }#1#);
}

app.UseHttpsRedirection();
        
app.MapControllers();

app.Run();*/
}