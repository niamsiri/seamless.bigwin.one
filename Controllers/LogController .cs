using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Reflection;

namespace seamless.bigwin.one.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "Server running";
        }

        [HttpGet("{name}")]
        public dynamic Get(String name)
        {
            dynamic json;
            
            using (StreamReader outputFile = new StreamReader(Path.Combine("./Logs", name + ".json")))
            {
                json = outputFile.ReadToEnd();
            }
            return json;
        }
    }
}
