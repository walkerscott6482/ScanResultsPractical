using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScanResults.Models;
using Serilog;

namespace ScanResults.Services
{
    public class GeoIPService
    {
        public GeoIPService()
        {
            Log.Information("GeoIPService started.");
        }

        public GeoIPData GetIPGeoLocation(string IP)
        {
            WebClient client = new WebClient();

            if (String.IsNullOrEmpty(IP))
            {
                Log.Error("GeoIPService.GetIPGeoLocation - Null or empty IP");
                throw new Exception("Null or empty IP");
            }

            try
            {
                Log.Debug("GeoIPService.GetIPGeoLocation: " + IP);
                string response = client.DownloadString("http://ip-api.com/json/" + IP);
                // Deserialize JSON response 
                GeoIPData ipdata = JsonConvert.DeserializeObject<GeoIPData>(response);

                if (ipdata.status == "fail")
                {
                    Log.Error("GeoIPService.GetIPGeoLocation - IPData status fail.");
                    throw new Exception("IPData status fail.");
                }

                return ipdata;
            }
            catch (Exception ex)
            {
                Log.Error("GeoIPService.GetIPGeoLocation - An error occurred: " + ex);
                throw;
            }
        }
    }
}
