using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Agricultural.Core.Models;

namespace Agricultural.Repo.Data.DataSeeding
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(PlanetContext DbContext)
        {
            try
            {
                #region PlantsInfo data seeding
                if (!DbContext.PlantsInfo.Any())
                {
                    var plantsData = File.ReadAllText("Data/DataSeeding/plants_data.json");
                    var plants = JsonSerializer.Deserialize<List<PlantsInfo>>(plantsData);

                    if (plants?.Count > 0)
                    {
                        await DbContext.Set<PlantsInfo>().AddRangeAsync(plants);
                        await DbContext.SaveChangesAsync();
                        Console.WriteLine("PlantsInfo data seeded successfully!");
                    }
                }
                #endregion

                #region Plant_Images data seeding
                if (!DbContext.PlantImages.Any())
                {
                    var plantImagesData = File.ReadAllText("Data/DataSeeding/PlantImages.json");
                    var plantImages = JsonSerializer.Deserialize<List<PlantImages>>(plantImagesData);

                    if (plantImages?.Count > 0)
                    {
                        await DbContext.Set<PlantImages>().AddRangeAsync(plantImages);
                        await DbContext.SaveChangesAsync();
                        Console.WriteLine("Plant_Images data seeded successfully!");
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ أثناء تهيئة البيانات: {ex.Message}", ex);
            }
        }
    }
}
