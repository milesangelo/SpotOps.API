
using SpotOps.Api.Models.Rest;

namespace SpotOps.Api.Services.Interfaces
{
    public interface ISpotRequestService
    {
        /// <summary>
        /// Add a SpotResponse to the underlying database context.
        /// </summary>
        /// <param name="spotResponse"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<SpotRequest> Add(SpotRequest spotResponse);
    }
  
}
