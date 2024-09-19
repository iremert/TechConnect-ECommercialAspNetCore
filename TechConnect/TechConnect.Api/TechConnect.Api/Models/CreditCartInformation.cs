namespace TechConnect.Api.Models
{
    public class CreditCartInformation
    {
        public string NameSurname { get; set; }
        public string CartNumber { get; set; } //16 haneli
        public string ExpirationDateMonth { get; set; }
        public string ExpirationDateYear { get; set; }
        public string CVV { get; set; }
    }
}
