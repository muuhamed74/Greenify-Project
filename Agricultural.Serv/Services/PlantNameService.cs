using System;
using System.Net.Http;
using System.Threading.Tasks;
using Agricultural.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Agricultural.Serv.Services
{
    public class PlantNameService : IPlantNameService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://plant-classification-api-production-a03e.up.railway.app/";

        public PlantNameService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetPlantNameFromImageAsync(IFormFile imageFile)
        {
            try
            {
                Console.WriteLine($"Sending image to API: {_apiUrl}");
                Console.WriteLine($"Image file: {imageFile.FileName}, Size: {imageFile.Length} bytes, ContentType: {imageFile.ContentType}");
                
                using var content = new MultipartFormDataContent();
                using var imageContent = new StreamContent(imageFile.OpenReadStream());
                
                // Use 'imagefile' as the key name as required by the API
                content.Add(imageContent, "imagefile", imageFile.FileName);
                
                var response = await _httpClient.PostAsync(_apiUrl, content);
                
                Console.WriteLine($"API response status: {response.StatusCode}");
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API error response: {errorContent}");
                    return null;
                }
                
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API response: {responseString}");
                
                var plantNamePrediction = JsonConvert.DeserializeObject<PlantNamePrediction>(responseString);
                
                var result = plantNamePrediction?.Result;
                Console.WriteLine($"Extracted plant name: {result ?? "null"}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting plant name from image: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return null;
            }
        }
    }
}
