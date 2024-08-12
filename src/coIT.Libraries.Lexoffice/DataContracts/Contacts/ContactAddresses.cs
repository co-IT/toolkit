using Newtonsoft.Json;

namespace coIT.Libraries.LexOffice.DataContracts.Contacts;

public class ContactAddresses
{
    [JsonProperty("billing")]
    public List<ContactAddress> Billing;

    [JsonProperty("shipping")]
    public List<ContactAddress> Shipping;
}
