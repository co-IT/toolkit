namespace coIT.Libraries.Clockodo.Absences.Contracts;

public class ClockodoAbsenceType
    : IEquatable<ClockodoAbsenceType>,
        IEquatable<int>,
        IComparable,
        IComparable<ClockodoAbsenceType>
{
    public int Id { get; set; }
    public string DisplayText { get; set; }

    public bool Equals(ClockodoAbsenceType? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (IsTransient() || other.IsTransient())
            return false;

        return Id.Equals(other.Id);
    }

    public bool Equals(int other)
    {
        return this.Id == other;
    }

    private bool Equals(long other)
    {
        return this.Id == other;
    }

    public override bool Equals(object? obj)
    {
        if (obj is long || obj is int)
            return Equals((long)obj);

        return obj is ClockodoAbsenceType other && Equals(other);
    }

    private bool IsTransient()
    {
        return Id.Equals(default);
    }

    public static bool operator ==(ClockodoAbsenceType? a, ClockodoAbsenceType? b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(ClockodoAbsenceType a, ClockodoAbsenceType b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public int CompareTo(object? obj)
    {
        if (obj != null && obj is not ClockodoAbsenceType)
            throw new ArgumentException($"Object must be of type {nameof(ClockodoAbsenceType)}");

        return CompareTo(obj as ClockodoAbsenceType);
    }

    public int CompareTo(ClockodoAbsenceType? other)
    {
        return ReferenceEquals(other, null)
            ? 1
            : string.Compare(this.DisplayText, other.DisplayText, StringComparison.Ordinal);
    }

    public override string ToString()
    {
        return DisplayText;
    }
}
