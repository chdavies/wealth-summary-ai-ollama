using WealthSummary.Api.Application.Dtos;

namespace WealthSummary.Api.Application.Services.Contracts;

public interface ISummaryService
{
    Task<ClientSummaryResponse> BuildSummaryAsync(int clientId, CancellationToken cancellationToken = default);
}
