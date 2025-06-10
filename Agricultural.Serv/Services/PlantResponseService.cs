using System;
using System.Net.Http;
using System.Threading.Tasks;
using Agricultural.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Agricultural.Serv.Services
{
    public class PlantResponseService : IPlantResponseService
    {
        private readonly HttpClient _httpClient;
        private readonly IPlantAdditionalDataService _additionalDataService;

        public PlantResponseService(HttpClient httpClient, IPlantAdditionalDataService additionalDataService)
        {
            _httpClient = httpClient;
            _additionalDataService = additionalDataService;
        }

        public async Task<object> AnalyzeImageAsync(IFormFile imageFile)
        {
            // Get plant name and status from image
            var (plantName, status) = await GetPlantInfoFromImageAsync(imageFile);
            
            // Get additional data for the identified plant and status
            var plantData = await _additionalDataService.GetPlantDataAsync(plantName, status);
            return plantData;
        }
        
        public async Task<(string plantName, string status)> GetPlantInfoFromImageAsync(IFormFile imageFile)
        {
            try
            {
                using var content = new MultipartFormDataContent();
                using var stream = imageFile.OpenReadStream();
                content.Add(new StreamContent(stream), "imagefile", imageFile.FileName);

                var response = await _httpClient.PostAsync("https://greenify-production-b44f.up.railway.app/", content);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var prediction = JsonConvert.DeserializeObject<PlantPrediction>(responseString);

                var parts = prediction.Result.Split("___");
                if (parts.Length != 2)
                    throw new InvalidOperationException("Invalid prediction result format.");

                var plantName = parts[0];
                var status = parts[1];

                return (plantName, status);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error getting plant info from image: {ex.Message}");
                return (null, null);
            }
        }
    }
}
