using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace SharedModels.Models
{

    public class CrewMembersbatchFile
    {
        public List<CrewMemberFromFile> CrewMemberList { get; set; }
    }

    public class CrewMemberFromFile
    {
        public string Name { get; set; }

        public string LastName { get; set; }
        [EmailAddress]
        [MaxLength(200)]
        //unique
        public string Email { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
        public string ProfileImageUrl { get; set; }

        public LicenseFromFile License { get; set; }

        public MemberTypeFromFile MemberType { get; set; }
    }

    public class LicenseFromFile
    {
        public Guid LicenseId { get; set; }

        public string Name { get; set; } // max 100 char

        public string Description { get; set; }
    }

    public class MemberTypeFromFile
    {
        public Guid MemberTypeId { get; set; }

        public string Name { get; set; } // max 100 char

    }

}
