using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Whois.NET;
using ScanResults.Models;
using Serilog;

namespace ScanResults.Services
{
    public class ScanResultsService
    {
        public ScanResultsService()
        {
            Log.Information("ScanResultsService started.");
        }

        public async Task<string> scanRequest(string ipDomain, IEnumerable<string> services)
        {
            // Get basic scan request information
            ScanRequest sr = new ScanRequest();
            if (IsIPAddress(ipDomain) == true)
            {
                sr.ip = ipDomain;
                // Get the domain name
                sr.domain = GetDomainFromIPAddress(ipDomain);
            }
            else
            {
                sr.domain = ipDomain;
                // Get the ip address
                sr.ip = GetIPAddressFromDomain(ipDomain);
            }
            sr.hosttype = GetHostNameType(ipDomain);

            // Get requested services data
            foreach (var service in services)
            {
                if (service == "GeoIP")
                {
                    GeoIPService gs = new GeoIPService();
                    sr.geoip = gs.GetIPGeoLocation(sr.ip);
                }
                else if (service == "Ping")
                {
                    PingService ps = new PingService();
                    sr.ping = ps.GetPing(sr.ip);
                }
                else if (service == "Whois")
                {
                    WhoisService ws = new WhoisService();
                    sr.whois = ws.GetWhoisInformation(sr.ip);
                }
            }
            
            var jsonResult = JsonConvert.SerializeObject(sr);
            return jsonResult;
        }

        private bool IsIPAddress(string ipAddress)
        {
            bool retVal = false;

            try
            {
                Log.Information("ScanResultsService.IsIPAddress - check IP address.");
                IPAddress address;
                retVal = IPAddress.TryParse(ipAddress, out address);
            }
            catch (Exception ex)
            {
                Log.Error("ScanResultsService.IsIPAddress - An error occurred: " + ex);                
            }

            return retVal;
        }

        private string GetDomainFromIPAddress(string ipDomain)
        {
            var retVal = string.Empty;

            retVal = Dns.GetHostEntry(ipDomain).HostName;

            return retVal;
        }

        private string GetIPAddressFromDomain(string ipDomain)
        {
            var retVal = string.Empty;

            retVal = Dns.GetHostAddresses(ipDomain).ToString();
            return retVal;
        }

        private string GetHostNameType(string ipDomain)
        {
            var retVal = string.Empty;

            try
            {
                Log.Information("ScanResultsService.GetHostNameType - Retrieve host name type.");
                retVal = Uri.CheckHostName(ipDomain).ToString();
            }
            catch (Exception ex)
            {
                Log.Error("ScanResultsService.GetHostNameType - Unable to retrieve host name type.");
            }

            return retVal;
        }

    }
}
