using Newtonsoft.Json;

namespace coIT.Libraries.LexOffice.DataContracts.Invoice;

public class InvoiceTotalPrice
{
    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("totalNetAmount")]
    public decimal TotalNetAmount { get; set; }

    [JsonProperty("totalGrossAmount")]
    public decimal TotalGrossAmount { get; set; }

    [JsonProperty("totalTaxAmount")]
    public decimal TotalTaxAmount { get; set; }

    [JsonProperty("totalDiscountAbsolute")]
    public decimal TotalDiscountAbsolute { get; set; }
}
