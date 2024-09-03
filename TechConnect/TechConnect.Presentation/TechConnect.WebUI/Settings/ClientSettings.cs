namespace TechConnect.WebUI.Settings
{
    public class ClientSettings
    {
        public Client TechConnectVisitorClient { get; set; }
        public Client TechConnectUserClient { get; set; }
        public Client TechConnectAdminClient { get; set; }
    }

    public class Client
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
