namespace WealthSummary.Domain.Model;

public class FinancialStatus
{
    public int FinancialStatusId { get; set; }

    public int ClientId { get; set; } = default!;

    public decimal AnnualIncome { get; set; }

    public decimal AnnualExpenses { get; set; }

    public RiskAppetite RiskAppetite { get; set; } 

}
