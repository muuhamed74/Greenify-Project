//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using Agricultural.Core.Models;
//using Agricultural.Repo.Data;
//using Agricultural.Repo.Repositories;
//using Microsoft.AspNetCore.Http;

//namespace Agricultural.Serv.Services
//{
//    public interface PlantResponceService
//    {
//        Task<PlantResponse> ProcessPlantImageAsync(IFormFile image);
//    }

//    public class PlantService : PlantResponceService
//    {
//        private readonly HttpClient _httpClient;
//        private readonly string _flaskApiUrl = "https://greenify-production-706f.up.railway.app/"; 
//        private readonly IYourDatabaseService _dbService; // افترض إنك بتستخدم service لقاعدة البيانات

//        public PlantService(IHttpClientFactory httpClientFactory, IYourDatabaseService dbService)
//        {
//            _httpClient = httpClientFactory.CreateClient();
//            _dbService = dbService;
//        }

//        public async Task<PlantResponse> ProcessPlantImageAsync(IFormFile image)
//        {
//            // 1. إرسال الصورة لـ Flask API
//            using var content = new MultipartFormDataContent();
//            using var imageStream = image.OpenReadStream();
//            var imageContent = new StreamContent(imageStream);
//            imageContent.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);
//            content.Add(imageContent, "image", image.FileName);

//            var flaskResponse = await _httpClient.PostAsync(_flaskApiUrl, content);
//            flaskResponse.EnsureSuccessStatusCode();

//            // 2. قراءة الرد من Flask
//            var flaskResult = await flaskResponse.Content.ReadAsStringAsync();
//            var prediction = JsonSerializer.Deserialize<PlantPrediction>(flaskResult, new JsonSerializerOptions
//            {
//                PropertyNameCaseInsensitive = true
//            });

//            // 3. جلب البيانات الإضافية
//            var additionalData = await _dbService.GetPlantAdditionalDataAsync(prediction.PlantName, prediction.Disease);

//            // 4. تجميع الـ response
//            return new PlantResponse
//            {
//                PlantName = prediction.PlantName,
//                Disease = prediction.Disease,
//                Confidence = prediction.Confidence,
//                PlantDescription = additionalData.PlantDescription,
//                DiseaseTreatment = additionalData.DiseaseTreatment
//            };
//        }
//    }
//}
