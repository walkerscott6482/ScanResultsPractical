using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using ScanResults.Models;
using Serilog;

namespace ScanResults.Services
{
    public class PingService
    {
        public PingService()
        {
            Log.Information("PingService started.");
        }

        public PingData GetPing(string ipDomain)
        {
            if (String.IsNullOrEmpty(ipDomain))
            {
                Log.Error("PingService.GetPing - Null or empty IP");
                throw new Exception("Null or empty IP");
            }

            Log.Debug("PingService.GetPing: " + ipDomain);

            PingData pd = new PingData();
            try
            {
                Ping pingSend = new Ping();
                PingReply reply = pingSend.Send(ipDomain, 12000); //await pingSend.SendPingAsync(ipDomain);

                pd.address = reply.Address.ToString();
                pd.roundtriptime = reply.RoundtripTime.ToString();
                pd.buffer = reply.Buffer.Length.ToString();
                pd.df = reply.Options.DontFragment.ToString();
                pd.ttl = reply.Options.Ttl.ToString();
            }
            catch (Exception ex)
            {
                Log.Error("PingService.GetPing - An error occurred: " + ex);
                throw;
            }

            return pd;
        }
    }
}
