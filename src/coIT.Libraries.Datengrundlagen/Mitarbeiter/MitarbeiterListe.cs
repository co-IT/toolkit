using coIT.Libraries.Clockodo.TimeEntries.Contracts;

namespace coIT.Libraries.Datengrundlagen.Mitarbeiter
{
    public class MitarbeiterListe : List<Mitarbeiter>
    {
        public MitarbeiterListe ClockodoMitarbeiterHinzufügen(List<UserWithTeam> clockodoNutzer)
        {
            var clockodoMitarbeiter = clockodoNutzer.Select(nutzer => new Mitarbeiter
            {
                Aktiv = nutzer.Active,
                Name = nutzer.Name.Replace($"{nutzer.Number}:", ""),
                Nummer = int.Parse(nutzer.Number ?? "0"),
                Team = nutzer.Team
            });

            var liste = new MitarbeiterListe();
            liste.AddRange(this);
            liste.AddRange(clockodoMitarbeiter);

            return liste;
        }
    }
}
