using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanResults.Models
{
    public class PingData
    {
        public string address { get; set; }
        public string roundtriptime { get; set; }
        public string ttl { get; set; }
        public string df { get; set; }
        public string buffer { get; set; }
    }
}
