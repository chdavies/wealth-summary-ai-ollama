using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using WealthSummary.Api.Application.Services;
using WealthSummary.Infrastructure;
using WealthSummary.Infrastructure.Data;

namespace WealthSummary.Api;

static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add CORS for Angular development
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularDev", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        builder.Services.AddServices(builder.Configuration);
        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            await ApplyMigrationsAndSeed(app.Services);

            app.UseSwagger();
            app.UseSwaggerUI();
            
            // Use CORS in development
            app.UseCors("AllowAngularDev");
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseRouting();
        app.MapControllers();

        await app.RunAsync();
    }


    static async Task ApplyMigrationsAndSeed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WealthDbContext>();

        try
        {
            // For demo with SQLite, just ensure database is created
            context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            // Log the error but continue - this is a demo
            Console.WriteLine($"Database setup error (continuing anyway): {ex.Message}");
        }

        try
        {
            await DatabaseSeeder.SeedAsync(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seeding error (continuing anyway): {ex.Message}");
        }
    }
}