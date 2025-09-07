using System.Text;
using WealthSummary.Domain.Model;

namespace WealthSummary.Api.Application.Services;

public class PromptBuilder
{
    public string BuildSystemPrompt()
    {
        return """
        You are a professional financial adviser. I will provide you with structured information about a client, including their assets, 
        liabilities, income and expenditure, pension details, financial goals, and notes from the last meeting with them.  

        Your task is to produce a clear, concise, and professional summary of the client review meeting that I can send directly to the client.  

        Requirements:  
        - Output must be in valid JSON format only.  
        - Use the following JSON schema consistently:  
                {
          "client_summary": {
            "financial_position": {
              "assets": "string",
              "liabilities": "string",
              "income_expenditure": "string",
              "pensions": "string"
            },
            "progress_since_last_meeting": "string",
            "financial_goals": "string",
            "recommendations_and_next_steps": "string",
            "overall_summary": "string"
          }
        }

        - Each field should contain clear, client-friendly text written in a professional but approachable tone.  
        - Avoid financial jargon where possible.  
        - Length of the overall_summary must keep the total response under 1000 words.
                
        - Do not include any text outside the JSON.
        """;
    }

    //public string BuildSystemPrompt()
    //{
    //    return """
    //    You are a helpful financial adviser that writes concise, accurate summaries for client annual reviews.
    //    - Always be factual and conservative.
    //    - When numbers are provided, cite them explicitly with currency and dates if available.
    //    - If information is missing, say so explicitly.
    //    - Provide insights and recommendations based on the data.
    //    - Provide comprehensive summary notes including key discussion points and recommended actions.
    //    - Avoid jargon; use clear, simple language.
    //    - Keep the summary under 1000 words.
    //    - Output MUST be valid JSON following this schema:
    //      {
    //        "client_overview": "string",
    //        "wealth_summary": {
    //          "total_assets": "string",
    //          "total_liabilities": "string",
    //          "net_worth_now": "string",
    //          "trend": "string",
    //          "liabilities_summary": "string"
    //        },
    //        "financial_status_summary": {
    //          "income": "string",
    //          "expenses": "string",
    //          "risk_profile": "string"
    //        },
    //        "summary_notes": "string",
    //        "key_risks": ["string"],
    //        "data_freshness": "string",
    //        "caveats": ["string"]
    //      }
    //    - Do not include any text outside the JSON.
    //    """;
    //}

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
            foreach (var asset in client.Assets)
            {
                sb.AppendLine($"- {asset.AssetType}: {asset.Description}: {asset.Value:C}");
            }
        }

        sb.AppendLine("== Liabilities ==");
        if (client.Liabilities.Count == 0)
        {
            sb.AppendLine("No liability records available.");
        }
        else
        {
            foreach (var liability in client.Liabilities)
            {
                sb.AppendLine($"- {liability.LiabilityType}: {liability.Description}: {liability.Value:C}");
            }

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
        }

        sb.AppendLine();
        sb.AppendLine("== Financial Goals ==");
        if (client.FinancialGoals.Count == 0)
        {             
            sb.AppendLine("No financial goals available.");
        }
        else
        {
            foreach (var goal in client.FinancialGoals)
            {
                sb.AppendLine($"- {goal.Description} by {goal.TargetDate:yyyy-MM-dd})");
            }
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
