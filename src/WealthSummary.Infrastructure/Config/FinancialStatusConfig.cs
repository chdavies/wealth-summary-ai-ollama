namespace WealthSummary.Infrastructure.Config;

public class FinancialStatusConfig : IEntityTypeConfiguration<FinancialStatus>
{
    public void Configure(EntityTypeBuilder<FinancialStatus> builder)
    {
        builder.ToTable("FinancialStatuses");

        builder.HasKey(fs => fs.FinancialStatusId);

        builder.Property(fs => fs.FinancialStatusId)
            .ValueGeneratedOnAdd();

        builder.Property(fs => fs.ClientId)
            .IsRequired();

        builder.Property(fs => fs.AnnualIncome)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(fs => fs.AnnualExpenses)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(fs => fs.RiskAppetite)
            .IsRequired();

        builder.Property(l => l.Goals)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne<Client>()
            .WithMany(c => c.FinancialStatuses)
            .HasForeignKey(fs => fs.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
