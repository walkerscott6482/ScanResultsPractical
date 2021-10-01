using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog;

namespace ScanResults.Services
{
    public class ValidationService
    {
        public ValidationService()
        {

        }

        public bool validateRequest(string request)
        {
            var retVal = false;

            if (checkDomain(request) == true)
            {
                if (checkHostType(request) == true)
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        private bool checkDomain(string ipDomain)
        {
            var retVal = false;

            if (ipDomain == string.Empty)
            {
                return false;
            }

            try
            {
                Log.Information("ValidationService.checkDomain - Check for valid domain.");
                retVal = Uri.CheckHostName(ipDomain) != UriHostNameType.Unknown;
            }
            catch (Exception ex)
            {
                Log.Error("ValidationService.validateDomain - Unable to check for valid domain.");
            }

            return retVal;
        }

        private bool checkHostType(string ipDomain)
        {
            var retVal = false;

            if (ipDomain == string.Empty)
            {
                return retVal;
            }

            try
            {
                // Host name can be Basic, Dns, IPv4, IPv6, or unknown
                Log.Information("ValidationService.checkHostType - Check host type.");
                UriHostNameType hostType = Uri.CheckHostName(ipDomain);
                retVal = true;
            }
            catch (Exception ex)
            {
                Log.Error("ValidationService.checkHostType - Error checking for valid domain: " + ex);
            }

            return retVal;
        }
    }
}
