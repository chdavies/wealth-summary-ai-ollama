using System.Text.Json;
using WealthSummary.Api.Application.Dtos;
using WealthSummary.Api.Application.Services.Contracts;
using WealthSummary.Domain.Repositories;


namespace WealthSummary.Api.Application.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly IClientRepository _clientRepository;
        private readonly OllamaClient _ollama;
        private readonly IConfiguration _config;

        public SummaryService(IClientRepository clientRepository,
                              OllamaClient ollama,
                              IConfiguration config) 
        {
            _clientRepository = clientRepository;
            _ollama = ollama;
            _config = config;
        }

        public async Task<ClientSummaryResponse?> BuildSummaryAsync(int clientId, CancellationToken cancellationToken = default)
        {
            // Fetch Client Data
            var client = await _clientRepository.GetByIdAsync(clientId);
            if (client == null) return null;

            // Build Prompts
            var promptBuilder = new PromptBuilder();
            var systemPrompt = promptBuilder.BuildSystemPrompt();
            var userPrompt = await promptBuilder.BuildUserPrompt(client);

            // Call Ollama
            var useJsonFormat = _config.GetValue<bool?>("Ollama:UseJsonFormat") ?? true;
            var jsonText = await _ollama.GetStructuredSummaryAsync(systemPrompt, userPrompt, useJsonFormat, cancellationToken);

            // Parse and Return
            try
            {
                var summary = JsonSerializer.Deserialize<ClientSummaryResponse>(jsonText, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return summary;
            }
            catch
            {
                // Fallback: Return as raw text if JSON parsing fails
                return new ClientSummaryResponse
                {
                    //ClientOverview = "Parsing failed; returning raw text in 'Caveats'",
                    Caveats = new List<string> { "Model did not return valid JSON.", "Raw output: " + Truncate(jsonText, 4000) }
                };
            }
        }

        private static string Truncate(string input, int max)
        => input.Length <= max ? input : input.Substring(0, max) + "...";

    }
}
