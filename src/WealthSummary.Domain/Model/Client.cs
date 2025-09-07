namespace WealthSummary.Domain.Model;

public class Client
{
    public int ClientId { get; set; } = default!;

    public string FullName { get; set; } = default!;

    public DateTime DateOfBirth { get; set; }

    public MaritalStatus MaritalStatus { get; set; }

    public IList<Asset> Assets { get; set; } = new List<Asset>();

    public IList<Liability> Liabilities { get; set; } = new List<Liability>();

    public IList<MeetingNote> MeetingNotes { get; set; } = new List<MeetingNote>();

    public IList<FinancialStatus> FinancialStatuses { get; set; } = new List<FinancialStatus>();

    public IList<FinancialGoal> FinancialGoals { get; set; } = new List<FinancialGoal>();

    public IList<Pension> Pensions { get; set; } = new List<Pension>();
}
