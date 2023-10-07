using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using APIRecieveAF.Services.Interfaces;
using SharedModels.Models;

namespace APIRecieveAF
{
    public class APIRecieveAF
    {


        private readonly ILogger<APIRecieveAF> _logger;
        private readonly IHttpTriggerService _triggerService;

        public APIRecieveAF(ILogger<APIRecieveAF> log, IHttpTriggerService httpTriggerService)
        {
            _logger = log;
            _triggerService = httpTriggerService;
        }

        [FunctionName("APIRecieveAF")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody(contentType: "application/json; charset=utf-8", bodyType: typeof(RawPersonRequest), Description = "Spacemane request", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json; charset=utf-8", bodyType: typeof(ResponsePerson), Description = "Apllication Response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("APIRecieveAF Started processing.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                RawPersonRequest data = JsonConvert.DeserializeObject<RawPersonRequest>(requestBody);

                // Validate data and process logic...
                // Example: Check if the person is a veteran and reject if not.

                ResponsePerson response = new ResponsePerson();

                if (data.isVeteran)
                {
                    response.result = true;
                    response.applicationAccepted = ApplicationAccepted.Accepted;
                    response.message = "Application accepted. Thank you for your service!";

                    await _triggerService.sendApplication(data);
                }
                else
                {
                    response.result = false;
                    response.applicationAccepted = ApplicationAccepted.Rejected;
                    response.message = "Application rejected. Only veterans are eligible.";
                }

                var result = JsonConvert.SerializeObject(response, Formatting.None);

                return new OkObjectResult(result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Error while processing message: {ex.Message}");
                return new BadRequestObjectResult("Error while processing message");
            }
        }
    }
}

