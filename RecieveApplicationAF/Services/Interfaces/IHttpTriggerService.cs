using SharedModels.Models;
using System.Threading.Tasks;

namespace APIRecieveAF.Services.Interfaces
{
    public interface IHttpTriggerService
    {
        Task sendApplication(RawPersonRequest message);
    }
}
