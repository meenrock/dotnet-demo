using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace dotnet_demo.Models.User
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string userId { get; set; }
        public string username { get; set; }
        public string ErrorMSG { get; set; } = "";
        public string mobileNo { get; set; }
        [JsonIgnore]
        public string password { get; set; }
    }
}