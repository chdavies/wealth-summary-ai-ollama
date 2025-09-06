namespace WealthSummary.Api.Application.Dtos
{
    public class ClientSummaryResponse
    {
        public string? ClientOverview { get; set; }

        public WealthSummary? Wealth_Summary { get; set; }
        public FinancialStatusSummary? Financial_Status_Summary { get; set; }
        public MeetingNotesSummary? Meeting_Notes_Summary { get; set; }

        public List<string>? Key_Risks { get; set; }
        public List<string>? Recommended_Actions { get; set; }
        public string? Data_Freshness { get; set; }
        public List<string>? Caveats { get; set; }

        public class WealthSummary
        {
            public string? Net_Worth_Now { get; set; }
            public string? Trend { get; set; }
            public string? Asset_Allocation { get; set; }
            public string? Liabilities_Summary { get; set; }
        }

        public class FinancialStatusSummary
        {
            public string? Income { get; set; }
            public string? Expenses { get; set; }
            public string? Cashflow { get; set; }
            public string? Risk_Profile { get; set; }
            public string? Goals { get; set; }
        }

        public class MeetingNotesSummary
        {
            public List<string>? Key_Points { get; set; }
            public List<string>? Decisions { get; set; }
            public List<string>? Follow_Ups { get; set; }
        }
    }
}
