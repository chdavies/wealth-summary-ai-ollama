namespace WealthSummary.Infrastructure.Config;

public class LiabilityConfig : IEntityTypeConfiguration<Liability>
{
    public void Configure(EntityTypeBuilder<Liability> builder)
    {
        builder.ToTable("Liabilities");

        builder.HasKey(l => l.LiabilityId);

        builder.Property(l => l.LiabilityId)
            .ValueGeneratedOnAdd();

        builder.Property(l => l.ClientId)
            .IsRequired();

        builder.Property(l => l.LiabilityType)
            .IsRequired();

        builder.Property(l => l.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(l => l.Value)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne<Client>()
            .WithMany(c => c.Liabilities)
            .HasForeignKey(l => l.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
