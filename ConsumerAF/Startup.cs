using System;
using System.Net.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Polly.Retry;
using ConsumerAF.Services;
using ConsumerAF.Services.Interfaces;
using Azure.Messaging.ServiceBus;

[assembly: FunctionsStartup(typeof(ConsumerAF.Startup))]
namespace ConsumerAF;
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = builder.GetContext().Configuration;

        builder.Services.AddTransient<ISBService, SBService>();
        builder.Services.AddTransient(factory =>
        {
            return new ServiceBusClient(configuration["ServiceBusConnection"]);
        });

    }

 
}

