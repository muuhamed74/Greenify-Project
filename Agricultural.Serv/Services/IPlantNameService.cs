using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Agricultural.Serv.Services
{
    public interface IPlantNameService
    {
        Task<string> GetPlantNameFromImageAsync(IFormFile imageFile);
    }
}
