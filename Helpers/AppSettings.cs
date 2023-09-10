using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_demo.Helpers
{
    public class AppSettings
    {
        public string secret { get; set; }
        public string connection { get; set; }
        public string reportUrl { get; set; }
        public string senderId { get; set; }
        public string serverKey { get; set; }
    }
}
