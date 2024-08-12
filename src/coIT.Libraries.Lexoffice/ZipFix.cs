namespace coIT.Libraries.LexOffice
{
    internal class ZipFix
    {
        private static readonly Dictionary<string, int> countryCodeZipLength = new Dictionary<
            string,
            int
        >
        {
            { "DE", 5 },
            { "AT", 4 },
            { "CH", 4 },
            { "HR", 5 }
        };

        public static string AddLeadingZeroes(string? zip, string countryCode)
        {
            if (!countryCodeZipLength.ContainsKey(countryCode))
                return zip;

            var zipLength = countryCodeZipLength[countryCode];
            var notNullZip = zip ?? string.Empty;
            var paddedZip = notNullZip.PadLeft(zipLength, '0');
            return paddedZip;
        }
    }
}
