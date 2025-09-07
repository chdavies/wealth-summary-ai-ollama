using WealthSummary.Api.Application.Dtos;

namespace WealthSummary.Api.Application.Services.Contracts;

public interface ISummaryService
{
    Task<RawResponse> BuildSummaryAsync(int clientId, CancellationToken cancellationToken = default);
}
