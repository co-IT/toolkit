namespace coIT.Database.Entities.ValueObjects
{
    public class Address
    {
        public Address(string street, string zipCode, string country, string city)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ArgumentException($"Die Postleitzahl muss angegeben werden");

            if (zipCode.Length > 5)
                throw new ArgumentOutOfRangeException(
                    nameof(ZipCode),
                    $"Die Postleitzahl darf maximal 5 Stellen haben: {zipCode}"
                );

            if (!zipCode.All(char.IsDigit))
                throw new ArgumentOutOfRangeException(
                    nameof(ZipCode),
                    $"Die Postleitzahl darf nicht negativ sein: {zipCode}"
                );

            if (country.Any(char.IsDigit))
                throw new ArgumentException($"Das Land darf keine Zahlen enthalten: {country}");

            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentOutOfRangeException($"Die Straße muss angegeben werden");

            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentOutOfRangeException($"Das Land muss angegeben werden");

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentOutOfRangeException($"Die Stadt muss angegeben werden");

            Street = street;
            ZipCode = zipCode;
            Country = country;
            City = city;
            CountrySign = GetCountrySign(country);
        }

        public Address() { }

        public static Address NoData() =>
            new Address("Keine Angabe", "99999", "Deutschland", "Keine Angabe");

        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public char CountrySign { get; private set; }

        private char GetCountrySign(string country)
        {
            if (country.Equals("deutschland", StringComparison.OrdinalIgnoreCase))
                return 'I';

            string[] countryCodes =
            {
                "österreich",
                "belgien",
                "bulgarien",
                "zypern",
                "tschechien",
                "dänemark",
                "estland",
                "spanien",
                "finnland",
                "frankreich",
                "vereinigtes königreich",
                "england",
                "kroatien",
                "ungarn",
                "irland",
                "italien",
                "litauen",
                "luxemburg",
                "lettland",
                "malta",
                "niederlande",
                "polen",
                "portugal",
                "rumänien",
                "schweden",
                "slowenien",
                "slowakei"
            };

            if (countryCodes.Contains(country, StringComparer.OrdinalIgnoreCase))
                return 'E';

            return 'D';
        }
    }
}
