using System.Text;
using WealthSummary.Api.Application.Dtos;
using WealthSummary.Api.Application.Services.Contracts;
using WealthSummary.Domain.Model;
using WealthSummary.Domain.Repositories;
using static Azure.Core.HttpHeader;

namespace WealthSummary.Api.Application.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly IClientRepository _clientRepository;

        public SummaryService(IClientRepository clientRepository) 
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientSummaryResponse?> BuildSummaryAsync(int clientId, CancellationToken cancellationToken = default)
        {
            // Fetch Client Data
            var client = await _clientRepository.GetByIdAsync(clientId);
            if (client == null) return null;

            // Build Prompt
            var promptBuilder = new PromptBuilder();
            var systemPrompt = promptBuilder.BuildSystemPrompt();
            var userPrompt = await promptBuilder.BuildUserPrompt(client);   

            // Call Ollama

            throw new NotImplementedException();
        }

        
    }
}
