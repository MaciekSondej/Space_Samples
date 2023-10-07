using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Newtonsoft.Json.Serialization;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;

namespace SharedModels.Models
{
    [OpenApiExample(typeof(RawPersonRequestExample))]
    public class RawPersonRequest
    {
        [OpenApiProperty]
        public string name { get; set; }
        [OpenApiProperty]
        public string lastName { get; set; }
        [OpenApiProperty]
        public string email { get; set; }
        [OpenApiProperty]
        public string phone { get; set; }
        [OpenApiProperty]
        public string address { get; set; }
        [OpenApiProperty]
        public string dateOfBirth { get; set; }
        [OpenApiProperty]
        public Boolean isVeteran { get; set; }
        [OpenApiProperty]
        public string? ISOCountryOfMilitaryService { get; set; }
        [OpenApiProperty]
        public int? YearsOfMiltaryService { get; set; }
    }

    public class RawPersonRequestExample : OpenApiExample<RawPersonRequest>
    {
        public override IOpenApiExample<RawPersonRequest> Build(NamingStrategy namingStrategy = null)
        {

            this.Examples.Add(
                 OpenApiExampleResolver.Resolve(
                     "RawPersonRequestExample",
                     new RawPersonRequest()
                     {
                         name = "John",
                         lastName = "Doe",
                         email = "test@gmail.com",
                         phone = "123456789",
                         address = "1234 Main St",
                         dateOfBirth = "01/01/2000",
                         isVeteran = true,
                         ISOCountryOfMilitaryService = "USA",
                         YearsOfMiltaryService = 5
                     },
                     namingStrategy
                 ));

            return this;
        }
    }
}
