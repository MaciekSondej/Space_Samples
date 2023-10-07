using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Newtonsoft.Json.Serialization;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;

namespace SharedModels.Models
{
    [OpenApiExample(typeof(ResponsePersonExample))]
    public class ResponsePerson
    {
        [OpenApiProperty]
        public Boolean result { get; set; }
        [OpenApiProperty]      
        public ApplicationAccepted applicationAccepted { get; set; }
        [OpenApiProperty]
        public string message { get; set; }
    }


    public enum ApplicationAccepted
    {
        Accepted,
        Rejected
    }

    public class ResponsePersonExample : OpenApiExample<ResponsePerson>
    {
        public override IOpenApiExample<ResponsePerson> Build(NamingStrategy namingStrategy = null)
        {

            this.Examples.Add(OpenApiExampleResolver.Resolve("ResponsePersonExample",new ResponsePerson()
            {
                         result = true,
                         applicationAccepted = ApplicationAccepted.Accepted,
                         message = "Application accepted. Thank you for your service!"
            },namingStrategy));

            return this;
        }
    }
}
