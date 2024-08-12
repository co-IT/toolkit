using System.Collections.Immutable;
using System.Globalization;
using System.Text;
using coIT.Libraries.Clockodo.Absences;
using coIT.Libraries.Clockodo.Absences.Contracts;

Console.WriteLine("Clockodo API: User eingeben");
var user = Console.ReadLine()!;

Console.WriteLine("Clockodo API: Token eingeben");
var token = Console.ReadLine()!;

var clockodoConfiguration = new AbsencesServiceSettings(user, token, "https://my.clockodo.com/");

var absenceTypes = new List<ClockodoAbsenceType>
{
    new() { Id = 4, DisplayText = "Lohnfortzahlung ohne Arztbesuch" }, //GDI: 9
    new() { Id = -1, DisplayText = "Lohnfortzahlung mit Arztbesuch" }, //GDI: 8
    new() { Id = 15, DisplayText = "Krankengeldbezug" }, //GDI: 7
    new() { Id = -2, DisplayText = "Krankheit eines Kindes mit eAU" },
    new() { Id = 5, DisplayText = "Krankheit eines Kindes" }, //kam 2023 nicht vor
    new() { Id = 1, DisplayText = "Urlaub" }, //23
    new() { Id = 2, DisplayText = "Sonderurlaub" }, //kam 2023 nicht vor
    //prüfen, wenn es mal verwendet wird
    new() { Id = 10, DisplayText = "Sonderurlaub (unbezahlt)" },
    new() { Id = 7, DisplayText = "Mutterschutz" },
    new() { Id = 13, DisplayText = "Quarantäne (nur ganze Tage)" },
    new() { Id = 14, DisplayText = "Militär-/Ersatzdienst (nur ganze Tage)" },
    new() { Id = 11, DisplayText = "Krankheit (unbezahlt)" },
    new() { Id = 12, DisplayText = "Krankheit eines Kindes (unbezahlt)" },
    //definitiv irrelevant für diesen U secase
    new() { Id = 3, DisplayText = "Überstundenabbau" },
    new() { Id = 6, DisplayText = "Schule / Weiterbildung" },
    new() { Id = 8, DisplayText = "Home Office (Sollarbeitszeit gilt)" },
    new() { Id = 9, DisplayText = "Arbeit außer Haus (Sollarbeitszeit gilt)" }
};

var abwesenheitenFilter = new HashSet<int> { -1, -2, 1, 2, 4, 5, 7, 10, 11, 12, 15 };
var mitarbeiterIstKrank = new HashSet<int> { -1, 4, 11, 15 };
var kindIstKrank = new HashSet<int> { -2, 5, 7, 12 };
var abwesenheitenIgnorieren = new HashSet<int> { 3, 6, 8, 9 };
var pruefenWennVorkommt = new HashSet<int> { 7, 10, 13, 14, 11, 12 };
var gdiSchluessel = new Dictionary<int, int>
{
    { 4, 9 },
    { -1, 8 },
    { 15, 7 },
    { 1, 23 }
};

Func<Absence, string> ausgabeFehlzeit = abwesenheit =>
{
    var text = absenceTypes.Single(a => a.Id == abwesenheit.AbsenceType.Id).DisplayText;

    return gdiSchluessel.ContainsKey(abwesenheit.AbsenceType.Id)
        ? $"{gdiSchluessel[abwesenheit.AbsenceType.Id]} {text}"
        : text;
};

var year = 2023;
var start = new DateTime(year, 01, 01);
var end = new DateTime(year, 12, 31);
var periodFilter = ClockodoPeriodFilter.Create(start, end).Value;

var clockodoService = new AbsencesService(clockodoConfiguration, absenceTypes);
var employees = await clockodoService.AllEmployees();
var allAbsences = await clockodoService.AllAbsences(periodFilter);

if (allAbsences.Any(a => pruefenWennVorkommt.Contains(a.AbsenceType.Id)))
{
    Console.WriteLine(
        "Es kommen Abwesenheiten vor, die noch nicht in diesem Programm behandelt werden können."
    );
    Console.ReadKey();
    Environment.Exit(-1);
}

allAbsences = allAbsences
    .Where(a => !abwesenheitenIgnorieren.Contains(a.AbsenceType.Id))
    .ToImmutableList();

foreach (var employee in employees)
{
    var abwesenheitenDesMitarbeiters = allAbsences
        .Where(a => a.UserId == employee.Id)
        .Where(a => abwesenheitenFilter.Contains(a.AbsenceType.Id))
        .ToList();

    if (!abwesenheitenDesMitarbeiters.Any())
        continue;

    var ausgabe = new StringBuilder();

    var name = employee
        .Name.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)
        .Last()
        .Trim();
    var nameAusgeben = 0;

    for (var monat = 1; monat <= 12; monat++)
    {
        var abwesenheitenDesMonats = abwesenheitenDesMitarbeiters
            .Where(a => a.Start.Month == monat)
            .ToList();

        if (!abwesenheitenDesMonats.Any())
            //ausgabe.AppendLine($"Monat {monat:00}: Keine Einträge");
            continue;
        nameAusgeben += 1;

        if (nameAusgeben == 1)
        {
            ausgabe.AppendLine(name);
        }

        ausgabe.AppendLine(
            $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monat)} {year}"
        );

        var sonderurlaub = abwesenheitenDesMonats.Where(a => a.AbsenceType.Id is 2 or 10).ToList();
        if (sonderurlaub.Any())
        {
            var tage = sonderurlaub.Sum(a => a.Duration);
            ausgabe.AppendLine($"\tSonderulraub {tage:N1}");
        }

        var normalerUrlaub = abwesenheitenDesMonats.Where(a => a.AbsenceType.Id == 1).ToList();
        if (normalerUrlaub.Any())
        {
            var tage = normalerUrlaub.Sum(a => a.Duration);
            ausgabe.AppendLine($"\tgen. akt. Monat {tage:N1}");
        }
        else
        {
            ausgabe.AppendLine($"\tgen. akt. Monat {0:N1}");
        }

        var krank = abwesenheitenDesMonats
            .Where(a => mitarbeiterIstKrank.Contains(a.AbsenceType.Id))
            .ToList();
        var krankKind = abwesenheitenDesMonats
            .Where(a => kindIstKrank.Contains(a.AbsenceType.Id))
            .ToList();
        var fehlzeiten = krank
            .Concat(krankKind)
            .Concat(normalerUrlaub)
            .Concat(sonderurlaub)
            .OrderBy(f => f.Start)
            .ToList();

        if (!fehlzeiten.Any())
            continue;

        ausgabe.AppendLine("\tFehlzeiten/Urlaub");
        foreach (var fehlzeit in fehlzeiten)
        {
            ausgabe.AppendLine(
                $"\t\t{ausgabeFehlzeit(fehlzeit)}: {fehlzeit.Start.ToShortDateString()} bis {fehlzeit.End.ToShortDateString()}"
            );
        }

        //foreach (var eintrag in krank)
        //    ausgabe.AppendLine(
        //        $"\t\t{eintrag.AbsenceType.DisplayText}: {eintrag.Start.ToShortDateString()} bis {eintrag.End.ToShortDateString()}");

        //foreach (var eintrag in krankKind)
        //    ausgabe.AppendLine(
        //        $"\t\t{eintrag.AbsenceType.DisplayText}: {eintrag.Start.ToShortDateString()} bis {eintrag.End.ToShortDateString()}");

        //foreach (var eintrag in normalerUrlaub)
        //    ausgabe.AppendLine(
        //        $"\t\t{eintrag.AbsenceType.DisplayText}: {eintrag.Start.ToShortDateString()} bis {eintrag.End.ToShortDateString()}");

        ausgabe.AppendLine();
    }

    try
    {
        var zieldatei = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            $"{name} Lohnzettel.txt"
        );
        File.WriteAllText(zieldatei, ausgabe.ToString(), Encoding.UTF8);
        Console.WriteLine($"In Datei {zieldatei} geschrieben.");
    }
    catch (Exception e)
    {
        Console.WriteLine("FEHLER");
        Console.WriteLine(e.Message);
    }
}

Console.ReadKey();


/* Code von Jannes

var userSicknessDict = allAbsences
    .GroupBy(a => a.UserId)
    .ToDictionary(grp => grp.Key,
        grp => grp
            .Where(a => a.AbsenceType.Id == 4)
            .Sum(a => a.Duration));

foreach (var user in userSicknessDict)
{
    var userName = employees.FirstOrDefault(e => e.Id == user.Key).Name;
    Console.WriteLine($"{userName,-26}: {user.Value}");
}
*/
