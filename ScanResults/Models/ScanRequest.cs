using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanResults.Models
{
    public class ScanRequest
    {
        public string ip { get; set; }
        public string domain { get; set; }
        public string hosttype { get; set; }

        public PingData ping { get; set; }
        public WhoisData whois { get; set; }
        public GeoIPData geoip { get; set; }

    }
}
