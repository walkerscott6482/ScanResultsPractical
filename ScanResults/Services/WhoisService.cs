using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using ScanResults.Models;
using Serilog;
using Whois.NET;

namespace ScanResults.Services
{
    public class WhoisService
    {
        public WhoisService()
        {
            Log.Information("WhoisService started.");
        }
        public WhoisData GetWhoisInformation(string ipDomain)
        {
            if (String.IsNullOrEmpty(ipDomain))
            {
                Log.Error("WhoisService.GetWhoisInformation - Null or empty IP");
                throw new Exception("Null or empty IP");
            }

            Log.Debug("WhoisService.GetWhoisInformation: " + ipDomain);

            WhoisData wd = new WhoisData();
            try
            {
                // Whois
                var result = WhoisClient.Query(ipDomain);

                wd.address = ipDomain;
                wd.addressrange = result.AddressRange.ToString();
                wd.organizationname = result.OrganizationName;
                wd.respondedservers = result.RespondedServers.ToString();
            }
            catch (Exception ex)
            {
                Log.Error("WhoisService.GetWhoisInformation - An error occurred: " + ex);
                throw;
            }

            return wd;
        }
    }
}
