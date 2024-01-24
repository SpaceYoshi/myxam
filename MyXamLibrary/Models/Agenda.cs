namespace MyXamLibrary.Models;

public class Agenda(Guid id, string name, Dictionary<Guid, AgendaEvent>? events = default) : IEquatable<Agenda>
{
    public Guid Id { get; } = id;
    public string Name { get; set; } = name;
    public Dictionary<Guid, AgendaEvent> Events { get; } = events ?? new Dictionary<Guid, AgendaEvent>();

    public bool Equals(Agenda? other)
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
        return Equals((Agenda)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}