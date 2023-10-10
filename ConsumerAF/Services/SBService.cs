using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SharedModels.Models;
using ConsumerAF.Services.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumerAF.Services;
public class SBService : ISBService
{
   
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ILogger<SBService> _logger;
    private readonly string _sharedQueueName = "confirmationqueue";

    public SBService(
        ServiceBusClient serviceBusClient,
        ILogger<SBService> logger)
    {
        _serviceBusClient = serviceBusClient;
        _logger = logger;
    }

   

    public async Task SendApplicationToSB(string _confirmationMsg)
    {
        try
        {
            var sender = this._serviceBusClient.CreateSender(this._sharedQueueName);

            var sbMessage = new ServiceBusMessage
            {
                ContentType = "RawPersonRequest",
                Body = BinaryData.FromString(_confirmationMsg),

            };

            await sender.SendMessageAsync(sbMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while sending to Service Bus");
            throw;
        }
        
    }


}
