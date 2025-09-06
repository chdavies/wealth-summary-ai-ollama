using System.Text;
using WealthSummary.Domain.Model;

namespace WealthSummary.Api.Application.Services;

public class PromptBuilder
{
    public string BuildSystemPrompt()
    {
        return """
        You are a helpful financial analysis assistant that writes concise, accurate summaries for client reviews.
        - Always be factual and conservative.
        - When numbers are provided, cite them explicitly with currency and dates if available.
        - If information is missing, say so explicitly.
        - Output MUST be valid JSON following this schema:
          {
            "client_overview": "string",
            "wealth_summary": {
              "net_worth_now": "string",
              "trend": "string",
              "asset_allocation": "string",
              "liabilities_summary": "string"
            },
            "financial_status_summary": {
              "income": "string",
              "expenses": "string",
              "cashflow": "string",
              "risk_profile": "string",
              "goals": "string"
            },
            "meeting_notes_summary": {
              "key_points": ["string"],
              "decisions": ["string"],
              "follow_ups": ["string"]
            },
            "key_risks": ["string"],
            "recommended_actions": ["string"],
            "data_freshness": "string",
            "caveats": ["string"]
          }
        - Do not include any text outside the JSON.
        """;
    }

    public async Task<string> BuildUserPrompt(Client client)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Summarize the following client data. Keep it precise and actionable.");
        sb.AppendLine();
        sb.AppendLine("== Client Profile ==");
        sb.AppendLine($"ClientId: {client.ClientId}");
        sb.AppendLine($"Name: {client.FullName}");
        sb.AppendLine($"DOB: {client.DateOfBirth:yyyy-MM-dd}");
        sb.AppendLine();

        sb.AppendLine("== Assets ==");
        if (client.Assets.Count == 0)
        {
            sb.AppendLine("No asset records available.");
        }
        else
        {
            var totalAssets = client.Assets.Sum(a => a.Value);
            var totalLiabilities = client.Liabilities.Sum(l => l.Value);

            // TODO: do we need details of the assets and liabilities
            //foreach (var asset in client.Assets)
            //{
            //    sb.AppendLine($"
            //}

            sb.AppendLine($"Total Asset Value: {totalAssets:C}");
            sb.AppendLine($"Total Liabilities: {totalLiabilities:C}");
            sb.AppendLine($"Net Worth: {(totalAssets - totalLiabilities):C}");
        }
        sb.AppendLine();

        sb.AppendLine("== Financial Status ==");

        var fin = client.FinancialStatuses.FirstOrDefault();
        if (fin == null)
        {
            sb.AppendLine("No financial status available.");

        }
        else
        {
            sb.AppendLine($"Risk Appetite: {fin.RiskAppetite}");
            sb.AppendLine($"Income: {fin.AnnualIncome:C}");
            sb.AppendLine($"Expenses: {fin.AnnualExpenses:C}");
            sb.AppendLine($"Cashflow: {(fin.AnnualIncome - fin.AnnualExpenses):C}");
            if (!string.IsNullOrWhiteSpace(fin.Goals)) sb.AppendLine($"Goals: {fin.Goals}");
        }
        sb.AppendLine();

        sb.AppendLine("== Advisor Meeting Notes ==");

        var notes = client.MeetingNotes.FirstOrDefault();
        if (notes == null)
        {
            sb.AppendLine("No meeting notes available.");
        }
        else
        {
            foreach (var note in client.MeetingNotes)
            {
                sb.AppendLine($"[{note.MeetingDate:yyyy-MM-dd}] {note.Author ?? "Advisor"}:");
                sb.AppendLine(note.Notes?.Trim() ?? "");
            }
        }
        sb.AppendLine();

        sb.AppendLine($"Data Freshness: As of {DateTime.UtcNow:yyyy-MM-dd} (UTC)");
        sb.AppendLine("Return only JSON per the schema. No markdown, no extra commentary.");

        return sb.ToString();

    }
}
