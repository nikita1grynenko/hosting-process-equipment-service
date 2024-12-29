using HostingProcessEquipmentService.Api.Middlewares;
using HostingProcessEquipmentService.Application.Configuration;
using HostingProcessEquipmentService.Application.Contracts;
using HostingProcessEquipmentService.Application.Services;
using HostingProcessEquipmentService.Infrastructure.Context;
using HostingProcessEquipmentService.Infrastructure.Contracts;
using HostingProcessEquipmentService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace HostingProcessEquipmentService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Load user secrets in development environment
        if (builder.Environment.IsDevelopment())
        {
            builder.Configuration.AddUserSecrets<Program>();
        }
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin() 
                    .AllowAnyHeader() 
                    .AllowAnyMethod(); 
            });
        });

        // Configure DbContext with connection string from user secrets
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("AzureSqlConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'AzureSqlConnection' is not found.");
            }
            options.UseSqlServer(connectionString);
        });

        // Register repositories
        builder.Services.AddScoped<IContractRepository, ContractRepository>();

        // Register services
        builder.Services.AddScoped<IContractService, ContractService>();
        builder.Services.Configure<AzureQueueSettings>(builder.Configuration.GetSection("AzureQueue"));
        builder.Services.AddSingleton<IQueueService, QueueService>();
        builder.Services.AddHostedService<AzureQueueBackgroundService>();
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Hosting process equipment for production facilities API.",
                Version = "v1",
                Description = "API with API Key Authentication"
            });

            // Add API Key security definition
            c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "API Key needed to access endpoints. Use the format: X-Api-Key: {your_api_key}",
                In = ParameterLocation.Header,
                Name = "X-Api-Key",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "ApiKeyScheme"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            
            c.AddServer(new OpenApiServer
            {
                Url = "https://hostingprocessequipmentapi-a7ashuhxfqfvche0.polandcentral-01.azurewebsites.net",
                Description = "Production API"
            });
        });

        var app = builder.Build();
        
        app.UseCors("AllowAll");

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // Add API Key Middleware
        app.UseMiddleware<ApiKeyMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
