using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Dynamic;
public class Balance
{
    public string action { get; set; }
    public string userId { get; set; }
    public List<dynamic> txns { get; set; }
}
public class Bet
{
    public string action { get; set; }
    public List<dynamic> txns { get; set; }
}


public class ReponseBalance
{
    public string status { get; set; }
    public string userId { get; set; }
    public string balance { get; set; }
    public DateTime balanceTs { get; set; }

}

namespace seamless.bigwin.one.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SexyController : ControllerBase
    {

        [HttpPost]
        public dynamic Post([FromForm] IFormCollection data)
        {
            if (data["message"].FirstOrDefault() != null)
            {
                var jsonData = JsonSerializer.Deserialize<Balance>(data["message"].FirstOrDefault());
                dynamic responseBanace = new ExpandoObject();
                if (jsonData.action == "getBalance")
                {
                    responseBanace.status = "0000";
                    responseBanace.userId = jsonData.userId;
                    responseBanace.balance = "1000";
                    responseBanace.balanceTs = DateTime.Now;

                    using (StreamWriter outputFile = new StreamWriter(Path.Combine("./Logs", "sexy.json")))
                    {
                        dynamic logData = new ExpandoObject();
                        logData.action = jsonData.action;
                        logData.userId =jsonData.userId;
                        outputFile.WriteLine(JsonSerializer.Serialize(logData));
                    }

                }
                else if (jsonData.action == "bet" || jsonData.action == "cancelBet")
                {
                    responseBanace.status = "0000";
                    responseBanace.balance = "1000";
                    responseBanace.balanceTs = DateTime.Now;

                    using (StreamWriter outputFile = new StreamWriter(Path.Combine("./Logs", "sexy.json")))
                    {
                        dynamic logData = new ExpandoObject();
                        logData.action = jsonData.action;
                        logData.txns = jsonData.txns;
                        outputFile.WriteLine(JsonSerializer.Serialize(logData));
                    }
                }
                else if (jsonData.action == "settle" || jsonData.action == "unsettle" || jsonData.action == "voidBet")
                {
                    responseBanace.status = "0000";

                    using (StreamWriter outputFile = new StreamWriter(Path.Combine("./Logs", "sexy.json")))
                    {
                        dynamic logData = new ExpandoObject();
                        logData.action = jsonData.action;
                        logData.txns = jsonData.txns;
                        outputFile.WriteLine(JsonSerializer.Serialize(logData));
                    }
                }
                return responseBanace;
            }
            return null;
        }
    }
}
