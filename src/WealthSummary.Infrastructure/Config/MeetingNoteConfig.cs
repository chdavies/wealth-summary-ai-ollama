namespace WealthSummary.Infrastructure.Config;

public class MeetingNoteConfig : IEntityTypeConfiguration<MeetingNote>
{
    public void Configure(EntityTypeBuilder<MeetingNote> builder)
    {
        builder.ToTable("MeetingNotes");

        builder.HasKey(mn => mn.MeetingNoteId);

        builder.Property(mn => mn.MeetingNoteId)
            .ValueGeneratedOnAdd();

        builder.Property(mn => mn.ClientId)
            .IsRequired();

        builder.Property(mn => mn.Notes)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(mn => mn.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        builder.HasOne<Client>()
            .WithMany(c => c.MeetingNotes)
            .HasForeignKey(mn => mn.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
