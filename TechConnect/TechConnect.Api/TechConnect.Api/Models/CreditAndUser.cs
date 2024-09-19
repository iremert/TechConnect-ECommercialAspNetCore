namespace TechConnect.Api.Models
{
    public class CreditAndUser
    {
        public string NameSurname { get; set; }
        public string CartNumber { get; set; } //16 haneli
        public string ExpirationDateMonth { get; set; }
        public string ExpirationDateYear { get; set; }
        public string CVV { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Price { get; set; }
    }
}
