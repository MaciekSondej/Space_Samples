using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAProcessor.Services.Interfaces;
public interface ISBService
{
    Task SendApplicationToSB(RawPersonRequest request);
}
