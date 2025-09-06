namespace WealthSummary.Domain.Model;

public class Liability
{
    public int LiabilityId { get; set; }

    public int ClientId { get; set; }

    public LiabilityType LiabilityType { get; set; }

    public string Description { get; set; } = default!;

    public decimal Value { get; set; }
}
