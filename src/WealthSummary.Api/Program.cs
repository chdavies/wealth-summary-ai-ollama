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

        // Bind configuration
        var ollamaConfig = builder.Configuration.GetSection("Ollama");
        var baseUrl = ollamaConfig.GetValue<string>("BaseUrl") ?? "http://localhost:11434";

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApi();

        // HttpClient for Ollama
        builder.Services.AddHttpClient<OllamaClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        builder.Services.AddServices();
        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            await ApplyMigrationsAndSeed(app.Services);

            app.UseSwagger();
            app.UseSwaggerUI();
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

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        await DatabaseSeeder.SeedAsync(context);
    }
}