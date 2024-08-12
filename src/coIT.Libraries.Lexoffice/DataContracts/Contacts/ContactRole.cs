using Newtonsoft.Json;

namespace coIT.Libraries.LexOffice.DataContracts.Contacts;

public class ContactRole
{
    [JsonProperty("customer")]
    public ContactCustomerNumber? Number;
}
