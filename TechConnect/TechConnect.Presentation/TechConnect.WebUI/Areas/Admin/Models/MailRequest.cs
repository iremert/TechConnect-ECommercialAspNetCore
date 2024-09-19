namespace TechConnect.WebUI.Areas.Admin.Models
{
    public class MailRequest
    {
        public string SenderMail { get; set; } //burada gönderen statik yapıldı
        public string ReceiverMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
