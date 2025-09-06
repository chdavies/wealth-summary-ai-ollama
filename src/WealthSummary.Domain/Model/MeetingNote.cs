namespace WealthSummary.Domain.Model;

public class MeetingNote
{
    public int MeetingNoteId { get; set; }

    public int ClientId { get; set; } = default!;

    public DateTime MeetingDate { get; set; }

    public string Author { get; set; } = default!;

    public string Notes { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
