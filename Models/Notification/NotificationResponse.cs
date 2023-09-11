using Newtonsoft.Json;

namespace dotnet_demo.Models.Notification
{
    public class NotificationResponse
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
