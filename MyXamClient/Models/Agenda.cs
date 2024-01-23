namespace MyXamClient.Models;

public class Agenda(Guid id, string name, List<Event>? events = default) : IEquatable<Agenda>
{
    private Guid Id { get; } = id;
    private string Name { get; set; } = name;
    private List<Event>? Events { get; set; } = events;

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
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Agenda)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}