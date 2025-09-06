namespace WealthSummary.Domain.Model;

public class Asset
{
    public int AssetId { get; set; }

    public int ClientId { get; set; }

    public AssetType AssetType { get; set; }

    public string Description { get; set; } = default!;

    public decimal Value { get; set; }

}
