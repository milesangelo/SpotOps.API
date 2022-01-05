using System.Threading.Tasks;
using SpotOps.Api.Data;
using SpotOps.Api.Models;
using SpotOps.Api.Models.Rest;
using SpotOps.Api.Services.Interfaces;

namespace SpotOps.Api.Services
{
    public class SpotRequestService : ISpotRequestService
    {
        private readonly ApplicationDbContext _context;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public SpotRequestService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<SpotRequest> Add(SpotRequest spotResponse)
        {
            Spot newSpot = new Spot
            {
                Name = spotResponse.Name
            };
            await _context.AddAsync(newSpot);
            await _context.SaveChangesAsync();
            return spotResponse;
        }
    }
}