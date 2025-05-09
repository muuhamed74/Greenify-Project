using Agricultural.Core.DTOs;
using System.Threading.Tasks;

namespace Agricultural.Serv.Services
{
    public interface IPlantSearchService
    {
        Task<PlantDTO> GetPlantByNameAsync(string plantName);
    }
}
