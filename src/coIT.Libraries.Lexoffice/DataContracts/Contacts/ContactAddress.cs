using coIT.Libraries.LexOffice.DataContracts.Country;
using Newtonsoft.Json;

namespace coIT.Libraries.LexOffice.DataContracts.Contacts;

public class ContactAddress
{
    [JsonProperty("supplement")]
    public string Supplement;

    [JsonProperty("street")]
    public string Street;

    [JsonProperty("zip")]
    public string Zip;

    [JsonProperty("city")]
    public string City;

    [JsonProperty("countrycode")]
    internal string Countrycode;
    public CountryInformation Country { get; set; }
}
