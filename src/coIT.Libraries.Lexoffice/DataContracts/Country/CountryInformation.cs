using Newtonsoft.Json;

namespace coIT.Libraries.LexOffice.DataContracts.Country
{
    public class CountryInformation
    {
        [JsonProperty("countryCode")]
        public string Code { get; set; }

        [JsonProperty("countryNameEN")]
        public string NameEnglish { get; set; }

        [JsonProperty("countryNameDE")]
        public string Name { get; set; }

        [JsonProperty("taxClassification")]
        public CountryTaxClassification TaxClassification { get; set; }
    }
}
