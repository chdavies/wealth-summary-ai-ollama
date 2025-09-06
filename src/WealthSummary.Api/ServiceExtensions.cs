using WealthSummary.Api.Application.Services;
using WealthSummary.Api.Application.Services.Contracts;

namespace WealthSummary.Api
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Register application services here
            services.AddScoped<ISummaryService, SummaryService>();

            return services;
        }
    }
}
