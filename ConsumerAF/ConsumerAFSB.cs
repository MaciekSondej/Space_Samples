using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedModels.Models;

namespace ConsumerAF
{
    public class ConsumerAFSB
    {
        [FunctionName("ConsumerAFSB")]
        public void Run([ServiceBusTrigger("samplequeue1", Connection = "ServiceBusConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation("ConsumerAFSB Started processing.");

            try
            {

                RawPersonRequest data = JsonConvert.DeserializeObject<RawPersonRequest>(myQueueItem);
                log.LogInformation($"Data recieved: {myQueueItem}, has been sucessfully deserialized and processed");
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Error while processing message: myQueueItem");
                log.LogError(ex, $"Error cause: {ex.Message}");
                throw;
            }
        }
    }
}
