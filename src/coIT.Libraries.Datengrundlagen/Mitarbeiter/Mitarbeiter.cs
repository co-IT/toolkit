using System.ComponentModel;
using coIT.Libraries.Clockodo.TimeEntries.Contracts;

namespace coIT.Libraries.Datengrundlagen.Mitarbeiter;

public class Mitarbeiter
{
    public int Nummer { get; set; }
    public string Name { get; set; }
    public bool Aktiv { get; set; }

    [Browsable(false)]
    public Team Team { get; set; }

    public string TeamName => Team.Name;

    public override string ToString()
    {
        return $"{Nummer}: {Name}";
    }
}
