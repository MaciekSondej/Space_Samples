using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace APIRecieveAF
{
    public class SBConfirmationAF
    {
        [FunctionName("SBConfirmationAF")]
        public void Run([ServiceBusTrigger("confirmationqueue", Connection = "ServiceBusConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"SBConfirmationAF has been triggered");
            log.LogInformation($"SBConfirmationAF has recieved message: {myQueueItem}");
        }
    }
}
