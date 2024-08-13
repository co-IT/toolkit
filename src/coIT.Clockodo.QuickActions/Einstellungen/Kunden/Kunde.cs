using System.ComponentModel;
using coIT.Libraries.LexOffice.DataContracts.Country;
using Newtonsoft.Json;

namespace coIT.Clockodo.QuickActions.Einstellungen.Kunden;

public class Kunde
{
    public Kunde() { }

    public Kunde(
        int kundennummer,
        int debitorennummer,
        string name,
        string straße,
        string postleitzahl,
        string stadt,
        string land,
        string landesvorwahl,
        string typ,
        CountryTaxClassification ländersteuerklassifizierung,
        string id
    )
    {
        Kundennummer = kundennummer;
        Debitorennummer = debitorennummer;
        DebitorName = name;
        Straße = straße;
        Postleitzahl = postleitzahl;
        Stadt = stadt;
        Land = land;
        Landesvorwahl = landesvorwahl;
        Ländersteuerklassifizierung = ländersteuerklassifizierung;
        LoadedFromFile = false;
        Typ = typ;
        Id = id;
    }

    public int Kundennummer { get; set; }
    public string Typ { get; set; }
    public int Debitorennummer { get; set; }
    public string DebitorName { get; set; }

    public string Straße { get; set; }
    public string Postleitzahl { get; set; }
    public string Stadt { get; set; }
    public string Land { get; set; }
    public string Landesvorwahl { get; set; }

    [Browsable(false)]
    public CountryTaxClassification Ländersteuerklassifizierung { get; set; }

    [Browsable(false)]
    public string Id { get; set; }

    [JsonIgnore]
    [Browsable(false)]
    public bool LoadedFromFile { get; set; } = true;

    public Kunde MitDebitorennummer(int debitorennummer)
    {
        return new(
            Kundennummer,
            debitorennummer,
            DebitorName,
            Straße,
            Postleitzahl,
            Stadt,
            Land,
            Landesvorwahl,
            Typ,
            Ländersteuerklassifizierung,
            Id
        );
    }
}
