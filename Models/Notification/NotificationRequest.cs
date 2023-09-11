namespace dotnet_demo.Models.Notification
{
    public class NotificationRequest
    {
        public List<string> username { get; set; } = new List<string>();
        public string title { get; set; }
        public string body { get; set; }
    }
}
