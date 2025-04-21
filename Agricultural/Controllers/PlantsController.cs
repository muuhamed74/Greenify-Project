using Agricultural.Core.Models;
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

        public PlantsController(PlantsService plantsService, IMapper mapper)
        {
            _plantsService = plantsService;
            _mapper = mapper;
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
    }
}


//[HttpGet("search")]
//public async Task<IActionResult> Search([FromQuery] string query)
//{
//    if (string.IsNullOrWhiteSpace(query))
//    {
//        return BadRequest("ادخل اسم نبات صحيح");
//    }

//    var (plants, suggestions) = await _plantsService.SearchPlantsAsync(query);

//    var response = new
//    {
//        Suggestions = suggestions,
//        Results = _mapper.Map<IEnumerable<PlantInfoDTO>>(plants)
//    };

//    return Ok(response);
//}