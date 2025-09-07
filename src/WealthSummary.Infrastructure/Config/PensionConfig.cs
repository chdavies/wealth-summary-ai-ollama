namespace WealthSummary.Infrastructure.Config;

public class PensionConfig : IEntityTypeConfiguration<Pension>
{
    public void Configure(EntityTypeBuilder<Pension> builder)
    {
        builder.ToTable("Pensions");

        builder.HasKey(mn => mn.PensionId);

        builder.Property(mn => mn.PensionId)
            .ValueGeneratedOnAdd();

        builder.Property(mn => mn.ClientId)
            .IsRequired();

        builder.Property(mn => mn.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(fs => fs.Value)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne<Client>()
            .WithMany(c => c.Pensions)
            .HasForeignKey(mn => mn.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}