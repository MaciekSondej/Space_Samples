using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;
using SharedModels.Models;
using SAProcessor.Services.Interfaces;
using YamlDotNet.Serialization;

namespace SAProcessor
{
    public class BlobConsumerXML
    {
        private readonly ILogger<BlobConsumerXML> _logger;
        private readonly ISBService _ISBService;
        private readonly IContractMapperService _contractMapperService;

        public BlobConsumerXML(ILogger<BlobConsumerXML> log, ISBService SBService, IContractMapperService contractMapperService)
        {
            _logger = log;
            _ISBService = SBService;
            _contractMapperService = contractMapperService;
        }


        [FunctionName("BlobConsumerXML")]
        public void Run([BlobTrigger("%FeederContainer2%/{name}", Connection = "SAConnectionString")]Stream uploadedFile, string name, ILogger log)
        {
            string fileContent;
            ApplicantsListXML applicantsFromFile;
            ApplicantsList applicantsToSB;
            int counter = 0;
            log.LogInformation("BlobConsumerXML Started processing.");
            try
            {
                using (StreamReader streamReader = new StreamReader(uploadedFile))
                {
                    fileContent = streamReader.ReadToEnd();

                    //Deserialize from XML 
                    XmlSerializer serializer = new XmlSerializer(typeof(ApplicantsListXML));

                    using (StringReader reader = new StringReader(fileContent))
                    {
                        applicantsFromFile = (ApplicantsListXML)serializer.Deserialize(reader);
                    }
                }

                applicantsToSB =  _contractMapperService.MapApplicantsListFromXML(applicantsFromFile);

                //IF applicantsFromFile is not null then send all applicants to SB
                if (applicantsToSB != null)
                {
                    foreach (RawPersonRequest applicant in applicantsToSB.Applicants)
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


