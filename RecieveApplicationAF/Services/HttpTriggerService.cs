using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedModels.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using APIRecieveAF.Services.Interfaces;

namespace APIRecieveAF.Services
{
    public class HttpTriggerService : IHttpTriggerService
    {
        private readonly HttpClient httpClient;
        private readonly string endpoint;
        private readonly ILogger<HttpTriggerService> logger;

        public HttpTriggerService(HttpClient httpClient, 
                                  IConfiguration configuration, 
                                   ILoggerFactory loggerFactory)
        {
            this.httpClient = httpClient;
            this.endpoint = configuration["apiEndpoint"];
            this.logger = loggerFactory.CreateLogger<HttpTriggerService>();
        }

        public async Task sendApplication(RawPersonRequest message)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(endpoint, message);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    logger.LogWarning($"Error calling {endpoint} with status code {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                var serializedMessage = JsonConvert.SerializeObject(message);
                logger.LogError(e, "Error while processing message: {message}", serializedMessage);
                throw;
            }
        }
    }
}
