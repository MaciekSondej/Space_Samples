using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels.Models;

namespace SAProcessor.Services.Interfaces
{
    public interface IContractMapperService
    {
        ApplicantsList MapApplicantsListFromXMLInConstructor(ApplicantsListXML input);
        ApplicantsList MapApplicantsListFromXML(ApplicantsListXML input);

    }
}
