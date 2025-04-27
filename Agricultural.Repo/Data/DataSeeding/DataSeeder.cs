using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Agricultural.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Agricultural.Repo.Data.DataSeeding
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(PlanetContext DbContext)
        {
            try
            {
                // Remove existing data to ensure a clean slate
                DbContext.PlantImages.RemoveRange(DbContext.PlantImages);
                DbContext.PlantsInfo.RemoveRange(DbContext.PlantsInfo);
                await DbContext.SaveChangesAsync();
                Console.WriteLine("Cleared existing data from PlantInfos and PlantImages.");

                #region PlantsInfo data seeding
                string baseDir = AppContext.BaseDirectory;
                string plantsFilePath = Path.Combine(baseDir, "Data", "DataSeeding", "plants_seeding_data.json");

                if (!File.Exists(plantsFilePath))
                {
                    throw new FileNotFoundException($"Could not find seeding file at path: {plantsFilePath}");
                }

                var plantsData = File.ReadAllText(plantsFilePath);
                var plants = JsonSerializer.Deserialize<List<PlantsInfo>>(plantsData);

                if (plants?.Count > 0)
                {
                    Console.WriteLine($"Seeding {plants.Count} PlantsInfo records...");
                    int index = 1;
                    foreach (var plant in plants)
                    {
                        Console.WriteLine($"PlantInfo #{index}: Name={plant.PlantName}, ScientificName={plant.ScientificName}, CareLevel={plant.CareLevel}, Size={plant.Size}, Edibility={plant.Edibility}, About={plant.About}, Details={plant.Details?.ToString() ?? "null"}");
                        index++;
                    }
                    await DbContext.Set<PlantsInfo>().AddRangeAsync(plants);
                    await DbContext.SaveChangesAsync();
                    Console.WriteLine("PlantsInfo data seeded successfully!");
                }
                #endregion

                #region Plant_Images data seeding
                string imagesFilePath = Path.Combine(baseDir, "Data", "DataSeeding", "plants_images.json");

                if (!File.Exists(imagesFilePath))
                {
                    throw new FileNotFoundException($"Could not find seeding file at path: {imagesFilePath}");
                }

                var plantImagesData = File.ReadAllText(imagesFilePath);
                var plantImages = JsonSerializer.Deserialize<List<PlantImages>>(plantImagesData);

                if (plantImages?.Count > 0)
                {
                    Console.WriteLine($"Deserialized {plantImages.Count} PlantImages records...");
                    foreach (var image in plantImages)
                    {
                        Console.WriteLine($"PlantImage: Id={image.Id}, PlantsInfoId={image.PlantsInfoId}");
                    }

                    // Get all PlantsInfo IDs from the database
                    var validPlantIds = await DbContext.PlantsInfo.Select(p => p.Id).ToListAsync();
                    Console.WriteLine($"Valid PlantsInfo IDs in database: [{string.Join(", ", validPlantIds)}]");

                    // Filter PlantImages to only include those with valid PlantsInfoId
                    var validPlantImages = plantImages.Where(pi => validPlantIds.Contains(pi.PlantsInfoId)).ToList();

                    if (validPlantImages.Count > 0)
                    {
                        Console.WriteLine($"Seeding {validPlantImages.Count} valid PlantImages records...");
                        await DbContext.Set<PlantImages>().AddRangeAsync(validPlantImages);
                        await DbContext.SaveChangesAsync();
                        Console.WriteLine("Plant_Images data seeded successfully!");
                    }
                    else
                    {
                        Console.WriteLine("No valid PlantImages to seed (invalid PlantsInfoId values).");
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