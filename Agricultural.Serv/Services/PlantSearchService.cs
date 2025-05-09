using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Agricultural.Core.DTOs;
using Agricultural.Repo.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Agricultural.Serv.Services
{
    public class PlantSearchService : IPlantSearchService
    {
        private readonly PlanetContext _context;

        public PlantSearchService(PlanetContext context)
        {
            _context = context;
        }

        public async Task<PlantDTO> GetPlantByNameAsync(string plantName)
        {
            try
            {
                // Return null if plant name is empty
                if (string.IsNullOrEmpty(plantName))
                    return null;
                
                // Try to find the plant by name (directly using the name from API)
                var plant = await _context.Plant
                    .Where(p => p.Name.ToLower().Contains(plantName) || 
                           plantName.Contains(p.Name.ToLower()))
                    .FirstOrDefaultAsync();
                
                // Return null if no plant found
                if (plant == null)
                    return null;
                
                // Map all plant properties to the DTO
                var plantDto = new PlantDTO
                {
                    Name = plant.Name,
                    ScientificName = plant.ScientificName,
                    GrowingConditions = plant.GrowingConditions != null ? new GrowingConditionsDTO
                    {
                        Sunlight = plant.GrowingConditions.Sunlight,
                        Water = plant.GrowingConditions.Water,
                        SoilType = plant.GrowingConditions.SoilType
                    } : null,
                    Flower = plant.Flower != null ? new FlowerDTO
                    {
                        Color = plant.Flower.Color,
                        Morphology = plant.Flower.Morphology
                    } : null,
                    // Deserialize the JSON strings to lists
                    Vitamins = !string.IsNullOrEmpty(plant.VitaminsJson) ? 
                        JsonConvert.DeserializeObject<List<string>>(plant.VitaminsJson) : new List<string>(),
                    HealthBenefits = !string.IsNullOrEmpty(plant.HealthBenefitsJson) ? 
                        JsonConvert.DeserializeObject<List<string>>(plant.HealthBenefitsJson) : new List<string>()
                };
                
                return plantDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching for plant: {ex.Message}");
                return null;
            }
        }
    }
}
