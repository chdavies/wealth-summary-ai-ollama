using System.Buffers.Text;
using System.Net.Http.Headers;
using WealthSummary.Api.Application.Services;
using WealthSummary.Api.Application.Services.Contracts;

namespace WealthSummary.Api
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind configuration
            var ollamaConfig = configuration.GetSection("Ollama");
            var baseUrl = ollamaConfig.GetValue<string>("BaseUrl") ?? "http://localhost:11434";

            // HttpClient for Ollama
            services.AddHttpClient<OllamaClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.Timeout = TimeSpan.FromMinutes(5);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            // Register application services here
            services.AddScoped<ISummaryService, SummaryService>();
            services.AddScoped<PromptBuilder>();

            return services;
        }
    }
}
