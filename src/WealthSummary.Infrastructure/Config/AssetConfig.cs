namespace WealthSummary.Infrastructure.Config;

internal class AssetConfig : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable("Assets");

        builder.HasKey(a => a.AssetId);

        builder.Property(a => a.AssetId)
            .ValueGeneratedOnAdd();

        builder.Property(a => a.ClientId)
            .IsRequired();

        builder.Property(a => a.AssetType)
            .IsRequired();

        builder.Property(a => a.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Value)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne<Client>()
            .WithMany(c => c.Assets)
            .HasForeignKey(a => a.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
