namespace ManuellePhishingMails.Domain.Csv.Ziel.SendgridDto
{
    public class Mail
    {
        public List<Personalization> Personalizations { get; set; }
        public MailUser From { get; set; }
        public MailUser Reply_to { get; set; }
        public string Template_id { get; set; }
    }
}
