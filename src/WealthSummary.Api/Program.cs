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

        // Add CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp", policy =>
            {
                policy.WithOrigins("http://localhost:3000")
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
        }

        app.UseHttpsRedirection();
        
        // Enable CORS
        app.UseCors("AllowReactApp");

        app.UseAuthorization();
        app.UseRouting();
        app.MapControllers();

        await app.RunAsync();
    }


    static async Task ApplyMigrationsAndSeed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WealthDbContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        await DatabaseSeeder.SeedAsync(context);
    }
}