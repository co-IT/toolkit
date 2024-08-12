namespace coIT.Libraries.TimeCard.DataContracts;

public class TimeCardAbsenceType
    : IEquatable<TimeCardAbsenceType>,
        IEquatable<int>,
        IComparable,
        IComparable<TimeCardAbsenceType>
{
    public TimeCardAbsenceType(int id, string displayText)
    {
        Id = id;
        DisplayText = displayText;
    }

    public int Id { get; set; }
    public string DisplayText { get; set; }

    public int CompareTo(object? obj)
    {
        if (obj != null && obj is not TimeCardAbsenceType)
            throw new ArgumentException($"Object must be of type {nameof(TimeCardAbsenceType)}");

        return CompareTo(obj as TimeCardAbsenceType);
    }

    public int CompareTo(TimeCardAbsenceType? other)
    {
        return ReferenceEquals(other, null)
            ? 1
            : string.Compare(DisplayText, other.DisplayText, StringComparison.Ordinal);
    }

    public bool Equals(int other)
    {
        return Id == other;
    }

    public bool Equals(TimeCardAbsenceType? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (IsTransient() || other.IsTransient())
            return false;

        return Id.Equals(other.Id);
    }

    private bool Equals(long other)
    {
        return Id == other;
    }

    public override bool Equals(object? obj)
    {
        if (obj is long || obj is int)
            return Equals((long)obj);

        return obj is TimeCardAbsenceType other && Equals(other);
    }

    private bool IsTransient()
    {
        return Id.Equals(default);
    }

    public static bool operator ==(TimeCardAbsenceType? a, TimeCardAbsenceType? b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(TimeCardAbsenceType a, TimeCardAbsenceType b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return DisplayText;
    }
}
