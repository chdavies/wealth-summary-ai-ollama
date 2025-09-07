using Microsoft.EntityFrameworkCore;

namespace WealthSummary.Infrastructure.Data;

public class WealthDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "main";

    public WealthDbContext(DbContextOptions<WealthDbContext> options) : base(options)
    {
    }

    public DbSet<Asset> Assets { get; set; }

    public DbSet<Client> Clients { get; set; }

    public DbSet<FinancialGoal> FinancialGoals { get; set; }

    public DbSet<FinancialStatus> FinancialStatuses { get; set; }

    public DbSet<Liability> Liabilities { get; set; }

    public DbSet<MeetingNote> MeetingNotes { get; set; }

    public DbSet<Pension> Pensions { get; set; }



    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Automatically applies all IEntityTypeConfiguration<T> in the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WealthDbContext).Assembly);

        modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
        
    }
}
