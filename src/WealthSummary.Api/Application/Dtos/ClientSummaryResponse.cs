using static WealthSummary.Api.Application.Dtos.ClientSummaryResponse;

namespace WealthSummary.Api.Application.Dtos
{
    public class ClientSummaryResponse
    {
        
        public ClientSummary Wealth_Summary { get; set; } = new ClientSummary(); 

        public List<string>? Caveats { get; set; }

    }

    public class ClientSummary
    {
        public FinancialPositionSummary Financial_Position { get; set; } = new FinancialPositionSummary();
        public string? Progress_Since_Last_Meeting { get; set; }
        public string? Financial_Goals { get; set; }
        public string? Recommendations_And_Next_Steps { get; set; }
        public string? Overall_Summary { get; set; }
    }

    public class FinancialPositionSummary
    {
        public string? Assets { get; set; }
        public string? Liabilities { get; set; }
        public string? Income_Expenditure { get; set; }
        public string? Pensions { get; set; }
    }
}
