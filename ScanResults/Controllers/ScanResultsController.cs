using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScanResults.Services;
using Serilog;
using Serilog.AspNetCore;

namespace ScanResults.Controllers
{
    [Produces("application/json")]
    [Route("api/ScanResults")]
    [ApiController]
    public class ScanResultsController : ControllerBase
    {
        public ScanResultsController()
        {
            Log.Information("ScanResultsController started.");
        }

        // GET: api/ScanResults
        [HttpGet]
        public IEnumerable<string> Get([FromBody] IEnumerable<string> services, string scanRequest)
        {            
            if (String.IsNullOrEmpty(scanRequest))
            {
                return new string[] { "No request was made" };
            }
            else
            {
                // Validate request(s)
                ValidationService vs = new ValidationService();
                if (vs.validateRequest(scanRequest) == false)
                {
                    Log.Error("ScanResultsController.ScanResults - Unable to validate request.");
                    return new string[] { "Unable to validate request." };
                }

                ScanResultsService ss = new ScanResultsService();
                string result = ss.scanRequest(scanRequest, services).Result;

                return new string[] { result };
            }
        }
    }
}
