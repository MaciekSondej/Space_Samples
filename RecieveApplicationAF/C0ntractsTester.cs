using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedModels.Models;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace APIRecieveAF
{
    public static class ContractsTester
    {
        [FunctionName("ContractsTester")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
          
            //deserialize json to object
            //CrewMembersbatchFile crewMembersbatchFile = JsonConvert.DeserializeObject<CrewMembersbatchFile>(await new StreamReader(req.Body).ReadToEndAsync());
            //string output = JsonConvert.SerializeObject(crewMembersbatchFile);
            string output;
            try
            {
                CrewMembersbatchFileXML crewMembersbatchFile;

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                XmlSerializer serializer = new XmlSerializer(typeof(CrewMembersbatchFileXML));

                using (StringReader reader = new StringReader(requestBody))
                {
                    crewMembersbatchFile = (CrewMembersbatchFileXML)serializer.Deserialize(reader);
                }
                output = "DOne";
            }
            catch(Exception ex)
            {
                log.LogError(ex, $"Error while processing message: {ex.Message}");
                output = $"Error while processing message: {ex.Message}";

                return new BadRequestObjectResult(output);
            }


            return new OkObjectResult(output);
        }
    }
}
