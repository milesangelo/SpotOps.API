using SpotOps.Api.Models.Rest;
using SpotOps.Api.Services.Interfaces;
using System.Collections.Concurrent;

namespace SpotOps.Api.Services
{
    public class MockSpotResponseService : ISpotRequestService
    {
        private Dictionary<int, SpotRequest> _context = new Dictionary<int, SpotRequest>();

        /// <summary>
        /// Add SpotResponse to injected database context.
        /// </summary>
        /// <param name="spotResponse"></param>
        /// <returns></returns>
        public Task<SpotRequest> Add(SpotRequest spotResponse)
        {
            spotResponse.Id = _context.Count + 1;
             _context.Add(_context.Count + 1, spotResponse);
            return Task.FromResult<SpotRequest>(spotResponse);
        }
    }
}
