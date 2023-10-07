using System;
using System.Net.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Polly.Retry;
using SAProcessor.Services;
using SAProcessor.Services.Interfaces;
using Azure.Messaging.ServiceBus;

[assembly: FunctionsStartup(typeof(SAProcessor.Startup))]
namespace SAProcessor;
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = builder.GetContext().Configuration;

        builder.Services.AddTransient<IContractMapperService, ContractMapperService>();
        builder.Services.AddTransient<ISBService, SBService>();
        builder.Services.AddTransient(factory =>
        {
            return new ServiceBusClient(configuration["ServiceBusConnection"]);
        });

    }

    private AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        var delay = Backoff.ExponentialBackoff(TimeSpan.FromMilliseconds(100), retryCount: 3);

        var policy = HttpPolicyExtensions
              .HandleTransientHttpError()
              .WaitAndRetryAsync(delay);
        return policy;
    }
    private void SetBaseApiAddress(HttpClient client, IConfiguration configuration) => client.BaseAddress = new Uri(configuration["apiUrl"]);
}

