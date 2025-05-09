using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Agricultural.Serv.Services
{
    public interface IPlantResponseService
    {
        Task<object> AnalyzeImageAsync(IFormFile imageFile);
        
        // New method that only gets plant name and status from image
        Task<(string plantName, string status)> GetPlantInfoFromImageAsync(IFormFile imageFile);
    }


}
