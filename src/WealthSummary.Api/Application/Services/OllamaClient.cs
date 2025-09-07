using System.Text.Json;

namespace WealthSummary.Api.Application.Services;

public class OllamaClient
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public OllamaClient(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
    }

    public async Task<string> GetStructuredSummaryAsync(string systemPrompt, string userPrompt, bool useJsonFormat, CancellationToken ct)
    {
        var model = _config.GetValue<string>("Ollama:Model") ?? "llama3.1:8b";
        var temperature = _config.GetValue<double?>("Ollama:Temperature") ?? 0.2;
        var numCtx = _config.GetValue<int?>("Ollama:NumCtx") ?? 8192;

        var payload = new
        {
            model,
            messages = new object[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userPrompt }
            },
            stream = false,
            options = new
            {
                temperature,
                num_ctx = numCtx
            },
            // Setting "format": "json" can enforce JSON output on models that support it
            // If your model doesn't support it, set useJsonFormat=false in appsettings.json
            format = useJsonFormat ? "json" : null
        };

        using var resp = await _http.PostAsJsonAsync("/api/chat", payload, ct);
        resp.EnsureSuccessStatusCode();

        using var doc = await JsonDocument.ParseAsync(await resp.Content.ReadAsStreamAsync(ct), cancellationToken: ct);

        // For non-streaming responses, the assistant content is typically at:
        // { "message": { "role": "assistant", "content": "..." }, "done": true, ... }
        var root = doc.RootElement;

        if (root.TryGetProperty("message", out var message) &&
            message.TryGetProperty("content", out var content))
        {
            return content.GetString() ?? "{}";
        }

        // Some versions may return an array of events; handle gracefully
        // Fallback: try 'content' at root (unlikely) or return raw JSON
        if (root.TryGetProperty("content", out var content2))
        {
            return content2.GetString() ?? "{}";
        }

        return root.GetRawText();
    }
}
