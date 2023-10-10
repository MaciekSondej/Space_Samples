using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace SharedModels.Models
{
    [Serializable]
    [XmlRoot("CrewMemberList")] 
    public class CrewMembersbatchFileXML
    {
        [XmlElement("CrewMember")]
        public List<CrewMemberFromFileXML> CrewMemberList { get; set; }
    }

    [Serializable]
    public class CrewMemberFromFileXML
    {
        public string Name { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        public string Country { get; set; }
        public string ProfileImageUrl { get; set; }
        [XmlElement("License")]
        public LicenseFromFileXML License { get; set; }
        [XmlElement("MemberType")]
        public MemberTypeFromFileXML MemberType { get; set; }
    }

    [Serializable]
    public class LicenseFromFileXML
    {
        public Guid LicenseId { get; set; }

        public string Name { get; set; } // max 100 char

        public string Description { get; set; }
    }
    [Serializable]
    public class MemberTypeFromFileXML
    {
        public Guid MemberTypeId { get; set; }

        public string Name { get; set; } // max 100 char

    }

}
