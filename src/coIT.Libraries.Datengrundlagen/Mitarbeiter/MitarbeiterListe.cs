using coIT.Libraries.Clockodo.TimeEntries.Contracts;

namespace coIT.Libraries.Datengrundlagen.Mitarbeiter
{
    public class MitarbeiterListe : List<Mitarbeiter>
    {
        public MitarbeiterListe ClockodoMitarbeiterHinzufügen(List<UserWithTeam> clockodoBenutzer)
        {
            var adminNutzerId = 350599;
            var clockodoMitarbeiter = clockodoBenutzer
                .Where(user => user.Id != adminNutzerId)
                .Select(ZuMitarbeiter)
                .ToList();

            var liste = new MitarbeiterListe();
            liste.AddRange(this);
            liste.AddRange(clockodoMitarbeiter);

            return liste;
        }

        private static Mitarbeiter ZuMitarbeiter(UserWithTeam clockodoBenutzer)
        {
            var name = clockodoBenutzer.Name.Split(":")[1].Trim();
            var nummer = clockodoBenutzer.Name.Split(":")[0].Trim();

            return new Mitarbeiter()
            {
                Nummer = int.Parse(nummer),
                Name = name,
                Aktiv = clockodoBenutzer.Active,
                Team = clockodoBenutzer.Team
            };
        }
    }
}
