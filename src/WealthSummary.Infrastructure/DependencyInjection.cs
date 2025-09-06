using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WealthSummary.Infrastructure.Data;

namespace WealthSummary.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WealthDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("WealthSummaryDatabase")));

        // Add your infrastructure services here
        services.AddScoped<Domain.Repositories.IClientRepository, Repositories.ClientRepository>();
        return services;
    }
}
