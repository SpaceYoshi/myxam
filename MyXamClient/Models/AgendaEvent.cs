namespace MyXamClient.Models;

public class AgendaEvent(
    Guid id,
    Guid agendaId,
    string name,
    DateTimeOffset startTime,
    DateTimeOffset endTime,
    // Optional
    string? location = default,
    string? description = default,
    IEnumerable<string>? tags = default,
    EventPriority priority = EventPriority.Normal)
    : IEquatable<AgendaEvent>
{
    private Guid Id { get; } = id;
    private Guid AgendaId { get; set; } = agendaId;
    private string Name { get; set; } = name;
    private DateTimeOffset StartTime { get; set; } = startTime;
    private DateTimeOffset EndTime { get; set; } = endTime;
    private string? Location { get; set; } = location;
    private string? Description { get; set; } = description;
    private IEnumerable<string>? Tags { get; set; } = tags;
    private EventPriority Priority { get; set; } = priority;

    public bool Equals(AgendaEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((AgendaEvent) obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

public enum EventPriority
{
    High, Normal, Low
}
