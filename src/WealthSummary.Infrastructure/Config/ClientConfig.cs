namespace WealthSummary.Infrastructure.Config;

public class ClientConfig : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");

        builder.HasKey(c => c.ClientId);

        builder.Property(c => c.ClientId)
            .ValueGeneratedNever();

        builder.Property(c => c.FullName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.DateOfBirth)
            .IsRequired();
    }
}
