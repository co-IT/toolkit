using coIT.Libraries.Clockodo.TimeEntries.Contracts;

namespace coIT.Clockodo.QuickActions;

internal class HistorischerZeiteintrag : IEquatable<HistorischerZeiteintrag>
{
    public string Kunde { get; private set; }
    public string Projekt { get; private set; }
    public string Leistung { get; private set; }
    public string Beschreibung { get; private set; }

    private int _id;
    internal int _customerId;
    internal int? _projectId;
    internal int? _serviceId;

    public bool Equals(HistorischerZeiteintrag? other)
    {
        if (ReferenceEquals(null, other))
            return false;
        if (ReferenceEquals(this, other))
            return true;

        return _id == other._id;
    }

    public static HistorischerZeiteintrag ErzeugeAus(TimeEntry zeiteintrag)
    {
        var eintrag = new HistorischerZeiteintrag
        {
            Kunde = zeiteintrag.Customer?.Name ?? "- Kein Kundenname -",
            Projekt = zeiteintrag.ProjectName,
            Leistung = zeiteintrag.ServicesName,
            Beschreibung = zeiteintrag.Text,
            _id = zeiteintrag.Id,
            _projectId = zeiteintrag.ProjectID,
            _serviceId = zeiteintrag.ServiceID,
            _customerId = zeiteintrag.CostumerID
        };

        return eintrag;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != GetType())
            return false;
        return Equals((HistorischerZeiteintrag)obj);
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }
}
