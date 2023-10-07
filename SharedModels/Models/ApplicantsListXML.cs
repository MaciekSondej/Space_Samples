using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SharedModels.Models
{
    [Serializable]
    [XmlRoot("Applicants")]
    public class ApplicantsListXML
    {
        [XmlElement("Applicant")]
        public List<ApplicantXML> Applicants { get; set; }
    }

}