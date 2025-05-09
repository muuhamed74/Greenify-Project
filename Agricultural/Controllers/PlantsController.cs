using Agricultural.Core.DTOs;
using Agricultural.DTOs;
using Agricultural.Serv.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Agricultural.Controllers
{

    public class PlantsController : BaseController
    {
        private readonly PlantsService _plantsService;
        private readonly IMapper _mapper;
        private readonly IPlantResponseService _plantResponseService;
        private readonly IPlantAdditionalDataService _plantAdditionalDataService;
        private readonly IPlantNameService _plantNameService;
        private readonly IPlantSearchService _plantSearchService;

        public PlantsController(PlantsService plantsService, IMapper mapper, IPlantResponseService plantResponseService, 
            IPlantAdditionalDataService plantAdditionalDataService, IPlantNameService plantNameService, IPlantSearchService plantSearchService)
        {
            _plantsService = plantsService;
            _mapper = mapper;
            _plantResponseService = plantResponseService;
            _plantAdditionalDataService = plantAdditionalDataService;
            _plantNameService = plantNameService;
            _plantSearchService = plantSearchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlants()
        {
            var plants = await _plantsService.GetAllPlantsAsync();
            var plantsDto = _mapper.Map<IEnumerable<PlantInfoDTO>>(plants);
            return Ok(plantsDto);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] PlantSearchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Query))
            {
                return BadRequest("ادخل اسم نبات صحيح");
            }

            var (plants, suggestions) = await _plantsService.SearchPlantsAsync(request.Query);

            var response = new
            {
                Suggestions = suggestions,
                Results = _mapper.Map<IEnumerable<PlantInfoDTO>>(plants)
            };

            return Ok(response);
        }

        [HttpPost("analyze-image")]
        public async Task<IActionResult> AnalyzeImage([FromForm] ImageUploadDto dto)
        {
            if (dto?.Image == null || dto.Image.Length == 0)
                return BadRequest("No image file uploaded.");

            // Step 1: Get plant name and status from image using PlantResponseService
            var (plantName, status) = await _plantResponseService.GetPlantInfoFromImageAsync(dto.Image);
            if (string.IsNullOrEmpty(plantName) || string.IsNullOrEmpty(status))
                return NotFound("Could not identify plant or disease status from image.");

            // Step 2: Get detailed plant information using PlantAdditionalDataService
            var plantData = await _plantAdditionalDataService.GetPlantDataAsync(plantName, status);
            if (plantData == null)
                return NotFound($"Plant identified as '{plantName}' with status '{status}', but no detailed information found.");

            // Return both the identified information and the details
            var response = new
            {
                PlantName = plantName,
                Status = status,
                Details = plantData
            };

            return Ok(response);
        }

        [HttpPost("identify-plant")]
            public async Task<IActionResult> IdentifyPlant([FromForm] ImageUploadDto dto)
        {
            if (dto?.Image == null || dto.Image.Length == 0)
                return BadRequest("No image file uploaded.");

            try
            {
                // Step 1: Get plant name from image using PlantNameService
                var plantName = await _plantNameService.GetPlantNameFromImageAsync(dto.Image);
                
                // Log the plant name for debugging
                Console.WriteLine($"API identified plant name: {plantName}");
                
                if (string.IsNullOrEmpty(plantName))
                    return NotFound("API could not identify any plant from the image.");

                // Step 2: Get detailed plant information using PlantSearchService
                var plantDetails = await _plantSearchService.GetPlantByNameAsync(plantName);
                
                if (plantDetails == null)
                {
                    // This is the key issue - the plant was identified by the API but not found in your database
                    Console.WriteLine($"Plant '{plantName}' was identified by API but not found in database");
                    return NotFound($"Plant identified as '{plantName}', but no matching plant found in our database.");
                }

                // Return both the identified name and the details
                var response = new
                {
                    IdentifiedName = plantName,
                    Details = plantDetails
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in IdentifyPlant: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the image.");
            }
        }

    }
}