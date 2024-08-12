namespace coIT.Libraries.TimeCard
{
    public record TimeCardSettings
    {
        public string WebAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int NoExportGroup { get; set; }

        public TimeCardSettings(
            string webAddress,
            string username,
            string password,
            int noExportGroup
        )
        {
            WebAddress = webAddress;
            Username = username;
            Password = password;
            NoExportGroup = noExportGroup;
        }
    }
}
