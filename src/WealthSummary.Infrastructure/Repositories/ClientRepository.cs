using WealthSummary.Domain.Repositories;
using WealthSummary.Infrastructure.Data;

namespace WealthSummary.Infrastructure.Repositories;

internal class ClientRepository : IClientRepository
{
    private readonly WealthDbContext _context;

    public ClientRepository(WealthDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await Task.FromResult(_context.Clients.AsEnumerable());
    }

    public async Task<Client?> GetByIdAsync(int clientId)
    {
        return await _context.Clients
            .Include(p => p.Assets)
            .Include(p => p.Pensions)
            .Include(p => p.Liabilities)
            .Include(p => p.FinancialStatuses)
            .Include(p => p.FinancialGoals)
            .Include(p => p.MeetingNotes)
            .FirstOrDefaultAsync(c => c.ClientId == clientId);

    }
}
