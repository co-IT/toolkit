using System.Text;

namespace coIT.Libraries.Gdi.HumanResources;

public class GdiEntry
{
    private readonly GdiAbsenceType _entryType;
    private readonly int _personnelNummer;
    private double _amountOfDays;
    private DateTime _begin;
    private char _beginIsAHalfDay;
    private DateTime _end;
    private char _endIsAHalfDay;
    private int _month;
    private string _notice;
    private int _timesAbsentNo;

    public GdiEntry(int personalNummer, GdiAbsenceType abwesenheit)
    {
        _personnelNummer = personalNummer;
        _entryType = abwesenheit;

        //sicherheitshalber setze ich hier falsche Werte
        _begin = DateTime.Now.AddYears(-200);
        _end = DateTime.Now.AddYears(-200);
    }

    public GdiEntry SetFehlzeitennr(GdiAbsenceType abwesenheit)
    {
        _timesAbsentNo = abwesenheit.Id;

        return this;
    }

    public GdiEntry SetMonat(int month)
    {
        _month = month;
        return this;
    }

    public GdiEntry SetBeginn(DateTime begin)
    {
        if (begin.Month == _month - 1)
        {
            //es handelt sich um einen monatsübergreifenden Eintrag
            //das Beginndatum wird auf den ersten Tag des selektierten Monats festgelegt
            _begin = new DateTime(begin.Year, _month, 1);
        }
        else if (begin.Month == _month)
        {
            _begin = begin;
        }
        else
        {
            Console.WriteLine(
                "Achtung: Der Zeitraum für den Eintrag ist zu groß und wird deshalb nicht verarbeitet. "
                    + "Bitte den Mitarbeiter mit der Personalnr '{0}' und dem Anfangsdatum '{1}' prüfen",
                _personnelNummer,
                begin
            );

            throw new ApplicationException();
        }

        return this;
    }

    public GdiEntry SetEnde(DateTime end)
    {
        if (end.Month == _month + 1)
        {
            //es handelt sich um einen monatsübergreifenden Eintrag
            //das Endedatum wird auf den letzten Tag des selektierten Monats festgelegt
            _end = new DateTime(end.Year, _month + 1, 1).AddDays(-1);
        }
        else if (end.Month == _month)
        {
            _end = end;
        }
        else
        {
            Console.WriteLine(
                "Achtung: Der Zeitraum für den Eintrag ist zu groß und wird deshalb nicht verarbeitet. "
                    + "Bitte den Mitarbeiter mit der Personalnr '{0}' und dem Endedatum '{1}' prüfen",
                _personnelNummer,
                end
            );

            throw new ApplicationException();
        }

        return this;
    }

    //Todo: Wir arbeitet Timecard bei halben Tagen? Müssen halbe Tage immer separat gepflegt werden? Wie funktioniert das bei Stundenbasis?
    public GdiEntry SetGanzeTage(bool allDayEvent)
    {
        _beginIsAHalfDay = 'N';

        if (allDayEvent || _entryType.IsSickness)
            _endIsAHalfDay = 'N';
        else
            //_beginIsAHalfDay = (_begin.Hour > 10) ? 'J' : 'N';
            //_endIsAHalfDay = (_end.Hour < 16) ? 'J' : 'N';
            _endIsAHalfDay = 'J';

        return this;
    }

    //Todo: Wie liefert Timecard Bemerkungen? Schon mit Hochkommata? Kann man dort länger als 20 Zeichen pflegen? Gilt die 20 Zeichen Grenze noch?
    public GdiEntry SetBemerkungZuFehlzeit(string notice)
    {
        if (!_entryType.IsSickness)
            return this;

        if (string.IsNullOrWhiteSpace(notice))
            return this;

        if (notice.Length > 20)
            notice = notice[..20]; //das Feld in GDI darf nur 20 Zeichen lang sein

        _notice = $"\"{notice}\""; //In Hochkommata, da es sonst bei Leerzeichen Probleme gibt

        return this;
    }

    public GdiEntry SetAnzahlTage(double amountOfDays)
    {
        if (amountOfDays > 0)
            _amountOfDays = amountOfDays;

        return this;
    }

    public string GetCsvLine()
    {
        var result = new StringBuilder();
        var splitSign = ";";

        result.Append(DateTime.Now.ToString("yyyy") + splitSign); //aktuelles Jahr
        result.Append(DateTime.Now.ToString("MM") + splitSign); //aktueller Monat
        result.AppendFormat("{0}{1}", _personnelNummer, splitSign); //Personalnummer
        result.Append(splitSign); //Lohnartennummer
        result.Append(splitSign); //Text
        result.Append(splitSign); //Einheit
        result.Append(splitSign); //Satz
        result.Append(splitSign); //Faktor
        result.Append(splitSign); //Zuschlag
        result.Append(splitSign); //Betrag
        result.Append(splitSign); //Kostenstelle
        result.Append(splitSign); //Kostenträger
        result.AppendFormat("{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}", splitSign); //10 Statistikfelder
        //Übernahmekennzeichen, das automatisch vom Programm gesetzt wird <- muss ich das setzen?
        result.Append(splitSign);
        result.Append("F;"); //Buchungskennzeichen
        result.Append(splitSign); //aktueller Tag
        //result.Append(DateTime.Now.ToString("dd") + splitSign); //aktueller Tag
        result.Append(splitSign); //Buchungskennzeichen
        result.Append(splitSign); //Art der Arbeit
        result.Append(splitSign); //Arbeitsinfo
        result.AppendFormat("{0}{1}", _timesAbsentNo, splitSign); //Fehlzeitennummer
        result.AppendFormat("{0}{1}", _notice, splitSign); //Bemerkung zur Fehlzeit
        result.AppendFormat("{0}{1}", _begin.ToShortDateString(), splitSign); //Fehlzeiten Beginn
        result.AppendFormat("{0}{1}", _end.ToShortDateString(), splitSign); //Fehlzeiten Ende
        result.AppendFormat("{0}{1}", _beginIsAHalfDay, splitSign); //Halber Tag Beginn
        result.AppendFormat("{0}{1}", _endIsAHalfDay, splitSign); //Halber Tag Ende
        result.Append(splitSign); //Keine Führung auf der Lohnsteuerkarte
        result.Append(splitSign); //Arbeitsunfähigkeit durch Unfall verursacht
        result.Append(splitSign); //Arbeitunfähigkeitsbescheinigung liegt vor
        result.Append(splitSign); //Fehlzeiten fürs Baugewerbe

        return result.ToString();
    }
}
