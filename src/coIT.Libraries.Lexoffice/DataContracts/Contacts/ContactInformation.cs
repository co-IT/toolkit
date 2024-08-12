using Newtonsoft.Json;

namespace coIT.Libraries.LexOffice.DataContracts.Contacts;

public class ContactInformation
{
    [JsonProperty("id")]
    public string Id;

    [JsonProperty("roles")]
    public ContactRole Role;

    [JsonProperty("company")]
    public ContactCompany Company;

    [JsonProperty("addresses")]
    public ContactAddresses Addresses;
}
