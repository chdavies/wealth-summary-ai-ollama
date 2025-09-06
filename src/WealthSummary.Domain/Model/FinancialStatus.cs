namespace WealthSummary.Domain.Model;

public class FinancialStatus
{
    public int FinancialStatusId { get; set; }

    public int ClientId { get; set; } = default!;

    public decimal AnnualIncome { get; set; }

    public decimal AnnualExpenses { get; set; }

    public RiskAppetite RiskAppetite { get; set; } 

    public string? Goals { get; set; } // e.g., "Retire at 60; fund children's college"
}
