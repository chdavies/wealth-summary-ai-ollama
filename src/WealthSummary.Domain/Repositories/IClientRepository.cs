using WealthSummary.Domain.Model;

namespace WealthSummary.Domain.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();

    Task<Client?> GetByIdAsync(int clientId);
}
