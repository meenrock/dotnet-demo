using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_demo.Helpers
{
    public class LoggerModel
    {
        public string Application { get; set; }
        public string Machine { get; set; }
        public string MachineIpAddress { get; set; }
        public string RequestIpAddress { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public DateTime RequestTimestamp { get; set; }
        public DateTime ResponseTimestamp { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestUri { get; set; }
        public string ApiPath { get; set; }
        public string RequestContentType { get; set; }
        public string RequestMethod { get; set; }
        public string RequestURLParams { get; set; }
        public string RequestContentBody { get; set; }
        public string ResponseContentType { get; set; }
        public string ResponseContentBody { get; set; }
        public int ResponseStatusCode { get; set; }
        public string ResponseHeaders { get; set; }
        public string ChannelType { get; set; }
    }
}
