namespace WealthSummary.Infrastructure.Config;

public class FinancialGoalConfig : IEntityTypeConfiguration<FinancialGoal>
{
    public void Configure(EntityTypeBuilder<FinancialGoal> builder)
    {
        builder.ToTable("FinancialGoals");

        builder.HasKey(g => g.FinancialGoalId);

        builder.Property(g => g.FinancialGoalId)
            .ValueGeneratedOnAdd();

        builder.Property(g => g.ClientId)
            .IsRequired();

        builder.Property(g => g.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(g => g.TargetDate)
            .IsRequired();

        builder.HasOne<Client>()
            .WithMany(c => c.FinancialGoals)
            .HasForeignKey(g => g.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}