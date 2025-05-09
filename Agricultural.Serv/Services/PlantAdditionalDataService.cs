
using System.Threading.Tasks;
using Agricultural.Core.DTOs;
using Agricultural.Repo.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Agricultural.Serv.Services
{
    public class PlantAdditionalDataService : IPlantAdditionalDataService
    {
        private readonly PlanetContext _context;
        private readonly IMapper _mapper;

        public PlantAdditionalDataService(PlanetContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<object> GetPlantDataAsync(string plantName, string status)
        {
            if (status.ToLower() == "healthy")
            {
                var healthyPlant = await _context.PlantAdditionalData
                    .FirstOrDefaultAsync(p => p.PlantName == plantName && p.Status.ToLower() == "healthy");
                if (healthyPlant != null)
                {
                    var healthyResponse = _mapper.Map<HealthyPlantResponseDto>(healthyPlant);
                    healthyResponse.PlantName = plantName;
                    healthyResponse.Status = "healthy";
                    return healthyResponse;
                }
            }
            else
            {
                var unHealthyPlant = await _context.PlantAdditionalData
                    .FirstOrDefaultAsync(p => p.PlantName == plantName && p.Status.ToLower() != "healthy");
                if (unHealthyPlant != null)
                {
                    var response = _mapper.Map<PlantResponseDto>(unHealthyPlant);
                    response.PlantName = plantName;
                    response.Status = status;
                    return response;
                }
            }
            return null;
        }

    }
}

