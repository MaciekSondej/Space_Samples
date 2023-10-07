using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedModels.Models;
using SAProcessor.Services.Interfaces;

namespace SAProcessor
{
    public class BlobConsumerJSON
    {
        private readonly ILogger<BlobConsumerJSON> _logger;
        private readonly ISBService _ISBService;

        public BlobConsumerJSON(ILogger<BlobConsumerJSON> log, ISBService SBService)
        {
            _logger = log;
            _ISBService = SBService;
        }


        [FunctionName("BlobConsumerJSON")]
        public void Run([BlobTrigger("%FeederContainer1%/{name}", Connection = "SAConnectionString")]Stream uploadedFile, string name, ILogger log)
        {
            string fileContent;
            ApplicantsList applicantsFromFile;
            int counter = 0;
            log.LogInformation("BlobConsumerJSON Started processing.");
            try
            {
                using (StreamReader reader = new StreamReader(uploadedFile))
                {
                    fileContent = reader.ReadToEnd();
                    //Deserialize from JSON
                    applicantsFromFile = JsonConvert.DeserializeObject<ApplicantsList>(fileContent);
                }

                //IF applicantsFromFile is not null then send all applicants to SB
                if (applicantsFromFile != null)
                {
                    foreach (RawPersonRequest applicant in applicantsFromFile.Applicants)
                    {
                        _ISBService.SendApplicationToSB(applicant);
                        counter++;
                    }
                }
                _logger.LogInformation("Number of application sent to SB: {0}", name);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while processing File: {0}", name);
                _logger.LogError(ex, $"Error while processing File: {ex.Message}");
                throw;
            }

        }
    }
}


