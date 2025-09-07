namespace WealthSummary.Domain.Model;

public class Pension
{
    public int PensionId { get; set; }

    public int ClientId { get; set; }

    public string Description { get; set; } = default!;

    public decimal Value { get; set; }
}
