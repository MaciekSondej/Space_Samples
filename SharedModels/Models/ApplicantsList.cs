using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels.Models
{

    public class ApplicantsList
    {
        public List<RawPersonRequest> Applicants { get; set; }
    }

}