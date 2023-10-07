using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SharedModels.Models
{
    [Serializable]
    public class ApplicantXML
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public bool IsVeteran { get; set; }
        public string IsoCountryOfMilitaryService { get; set; }
        public int YearsOfMilitaryService { get; set; }
    }
}
