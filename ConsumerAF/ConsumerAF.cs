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

namespace ConsumerAF
{
    public static class ConsumerAF
    {
        [FunctionName("ConsumerAF")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("ConsumerAF Started processing.");
            Boolean result = false;

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                RawPersonRequest data = JsonConvert.DeserializeObject<RawPersonRequest>(requestBody);
                log.LogInformation($"Data recieved: {requestBody}, has been sucessfully deserialized and processed");
                result = true;
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Error while processing message: {ex.Message}");
            }

            if(result)
            {
                return new OkObjectResult("Data has been sucessfully deserialized and processed");
            }
            else
            {
                return new BadRequestObjectResult("Error while processing message");
            }
          
        }
    }
}
