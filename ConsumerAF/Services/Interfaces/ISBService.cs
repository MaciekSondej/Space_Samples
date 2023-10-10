using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerAF.Services.Interfaces;
public interface ISBService
{
    Task SendApplicationToSB(string _confirmationMsg);
}
