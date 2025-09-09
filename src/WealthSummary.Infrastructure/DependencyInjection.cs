using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WealthSummary.Infrastructure.Data;

namespace WealthSummary.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Use SQLite for demo purposes instead of SQL Server
        var connectionString = configuration.GetConnectionString("WealthSummaryDatabase");
        
        if (connectionString != null && connectionString.Contains("Server="))
        {
            // Convert SQL Server connection to SQLite for demo
            services.AddDbContext<WealthDbContext>(options =>
                options.UseSqlite("Data Source=wealth_demo.db"));
        }
        else
        {
            services.AddDbContext<WealthDbContext>(options =>
                options.UseSqlite(connectionString ?? "Data Source=wealth_demo.db"));
        }

        // Add your infrastructure services here
        services.AddScoped<Domain.Repositories.IClientRepository, Repositories.ClientRepository>();
        return services;
    }
}
