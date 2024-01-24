using System.Text.Json.Serialization;

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
    HashSet<string>? tags = default,
    EventPriority priority = EventPriority.Normal)
    : IEquatable<AgendaEvent>
{
    public Guid Id { get; } = id;
    public Guid AgendaId { get; set; } = agendaId;
    public string Name { get; set; } = name;
    public DateTimeOffset StartTime { get; set; } = startTime;
    public DateTimeOffset EndTime { get; set; } = endTime;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Location { get; set; } = location;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Description { get; set; } = description;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public HashSet<string>? Tags { get; set; } = tags;
    public EventPriority Priority { get; set; } = priority;

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
        return Equals((AgendaEvent)obj);
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
