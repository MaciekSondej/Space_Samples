using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAProcessor.Services.Interfaces;
using SharedModels.Models;


namespace SAProcessor.Services
{
    public class ContractMapperService : IContractMapperService
    {
        public ApplicantsList MapApplicantsListFromXML(ApplicantsListXML input)
        {
            ApplicantsList applicantsList = new ApplicantsList();

            applicantsList.Applicants = input.Applicants.Select(MapRawPersonRequest).ToList();
            return applicantsList;
        }

        public ApplicantsList MapApplicantsListFromXMLInConstructor(ApplicantsListXML input)
        {
            return new ApplicantsList
            {
                Applicants = input.Applicants.Select(x => new RawPersonRequest
                {
                    name = x.Name,
                    lastName = x.LastName,
                    email = x.Email,
                    phone = x.Phone,
                    address = x.Address,
                    dateOfBirth = x.DateOfBirth,
                    isVeteran = x.IsVeteran,
                    ISOCountryOfMilitaryService = x.IsoCountryOfMilitaryService,
                    YearsOfMiltaryService = x.YearsOfMilitaryService
                }).ToList()
            };
        }

        private RawPersonRequest MapRawPersonRequest(ApplicantXML input)
        {
            RawPersonRequest rawPersonRequest = new RawPersonRequest();


            rawPersonRequest.name = input.Name;
            rawPersonRequest.lastName = input.LastName;
            rawPersonRequest.email = input.Email;
            rawPersonRequest.phone = input.Phone;
            rawPersonRequest.address = input.Address;
            rawPersonRequest.dateOfBirth = input.DateOfBirth;
            rawPersonRequest.isVeteran = input.IsVeteran;
            rawPersonRequest.ISOCountryOfMilitaryService = input.IsoCountryOfMilitaryService;
            rawPersonRequest.YearsOfMiltaryService = input.YearsOfMilitaryService;

            return rawPersonRequest;
        }



    }
}
