using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedModels.Models;
using ConsumerAF.Services.Interfaces;

namespace ConsumerAF
{
    public class ConsumerAFSB
    {
        private readonly ILogger<ConsumerAFSB> _logger;
        private readonly ISBService _ISBService;

        public ConsumerAFSB(ILogger<ConsumerAFSB> log, ISBService SBService)
        {
            _logger = log;
            _ISBService = SBService;
        }



        [FunctionName("ConsumerAFSB")]
        public void Run([ServiceBusTrigger("samplequeue1", Connection = "ServiceBusConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation("ConsumerAFSB Started processing.");

            try
            {

                RawPersonRequest data = JsonConvert.DeserializeObject<RawPersonRequest>(myQueueItem);
                string logMsg = $"Data recieved: {myQueueItem}, has been sucessfully deserialized and processed";
                log.LogInformation(logMsg);

                _ISBService.SendApplicationToSB(logMsg);

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
