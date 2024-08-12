namespace ManuellePhishingMails.Domain.Csv.Ziel.SendgridDto
{
    public class Personalization
    {
        public List<MailUser> To { get; set; }
        public Dictionary<string, string> Dynamic_template_data { get; set; }
    }
}
