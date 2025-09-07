namespace WealthSummary.Domain.Model;

public class FinancialGoal
{
    public int FinancialGoalId { get; set; }

    public int ClientId { get; set; } = default!;

    public string? Description { get; set; } // e.g., "Retire at 60; fund children's college"

    public DateTime? TargetDate { get; set; }
}
