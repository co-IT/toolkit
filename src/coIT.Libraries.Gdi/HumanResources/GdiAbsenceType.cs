namespace coIT.Libraries.Gdi.HumanResources
{
    public class GdiAbsenceType
        : IEquatable<GdiAbsenceType>,
            IEquatable<int>,
            IComparable,
            IComparable<GdiAbsenceType>
    {
        public int Id { get; set; }
        public string DisplayText { get; set; }
        public bool IsSickness { get; set; }
        public bool IsHoliday { get; set; }

        public bool Equals(GdiAbsenceType? other)
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
            return Id == other;
        }

        private bool Equals(long other)
        {
            return Id == other;
        }

        public override bool Equals(object? obj)
        {
            if (obj is long || obj is int)
                return Equals((long)obj);

            return obj is GdiAbsenceType other && Equals(other);
        }

        private bool IsTransient()
        {
            return Id.Equals(default);
        }

        public static bool operator ==(GdiAbsenceType? a, GdiAbsenceType? b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(GdiAbsenceType a, GdiAbsenceType b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public int CompareTo(object? obj)
        {
            if (obj != null && obj is not GdiAbsenceType)
                throw new ArgumentException($"Object must be of type {nameof(GdiAbsenceType)}");

            return CompareTo(obj as GdiAbsenceType);
        }

        public int CompareTo(GdiAbsenceType? other)
        {
            return ReferenceEquals(other, null)
                ? 1
                : string.Compare(DisplayText, other.DisplayText, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return DisplayText;
        }
    }
}
