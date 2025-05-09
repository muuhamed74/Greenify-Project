using System.Text.Json;
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
                // إزالة البيانات الحالية لضمان نظافة البيانات
                DbContext.PlantImages.RemoveRange(DbContext.PlantImages);
                DbContext.PlantsInfo.RemoveRange(DbContext.PlantsInfo);
                DbContext.PlantAdditionalData.RemoveRange(DbContext.PlantAdditionalData);
                await DbContext.SaveChangesAsync();
                Console.WriteLine("Cleared existing data from PlantInfos and PlantImages.");

                #region PlantsInfo data seeding
                // الحصول على المسار الرئيسي للتطبيق
                string baseDir = Directory.GetParent(AppContext.BaseDirectory)?.FullName ?? AppContext.BaseDirectory;
                Console.WriteLine($"Base directory: {baseDir}");

                string plantsFilePath = Path.Combine(baseDir, "Data", "DataSeeding", "plants_seeding_data.json");
                Console.WriteLine($"Looking for plants_seeding_data.json at: {plantsFilePath}");

                if (!File.Exists(plantsFilePath))
                {
                    throw new FileNotFoundException($"Could not find seeding file at path: {plantsFilePath}");
                }

                var plantsData = File.ReadAllText(plantsFilePath);
                var plants = JsonSerializer.Deserialize<List<PlantsInfo>>(plantsData);

                if (plants?.Count > 0)
                {
                    Console.WriteLine($"Seeding {plants.Count} PlantsInfo records...");
                    await DbContext.Set<PlantsInfo>().AddRangeAsync(plants);
                    await DbContext.SaveChangesAsync();  // الآن لدينا الـ IDs الحقيقية للنباتات في الـ DB

                    var addedPlants = await DbContext.PlantsInfo.ToListAsync();
                    Console.WriteLine($"Successfully added {addedPlants.Count} PlantsInfo records. IDs: [{string.Join(", ", addedPlants.Select(p => p.Id))}]");
                }
                #endregion

                #region Plant_Images data seeding

                string imagesFilePath = Path.Combine(baseDir, "Data", "DataSeeding", "plants_images.json");
                Console.WriteLine($"Looking for plants_images.json at: {imagesFilePath}");

                if (!File.Exists(imagesFilePath))
                {
                    throw new FileNotFoundException($"Could not find seeding file at path: {imagesFilePath}");
                }

                var plantImagesData = File.ReadAllText(imagesFilePath);
                var plantImages = JsonSerializer.Deserialize<List<PlantImages>>(plantImagesData);

                if (plantImages?.Count > 0)
                {
                    Console.WriteLine($"Deserialized {plantImages.Count} PlantImages records...");

                    // الحصول على جميع الـ Ids الحقيقية من PlantsInfo
                    var validPlantIds = await DbContext.PlantsInfo.Select(p => p.Id).ToListAsync();

                    // تحديث الـ PlantsInfoId في الصور ليتناسب مع الـ Ids الحقيقية في قاعدة البيانات
                    foreach (var image in plantImages)
                    {
                        int originalIndex = image.PlantsInfoId; // هذا هو الرقم الذي كان في الـ JSON
                        if (originalIndex >= 1 && originalIndex <= validPlantIds.Count)
                        {
                            // ربط الصورة بالنبتة الصحيحة باستخدام الـ Id الحقيقي
                            image.PlantsInfoId = validPlantIds[originalIndex - 1];
                        }
                        else
                        {
                            Console.WriteLine($"Warning: Invalid PlantsInfoId {originalIndex} for image: {image.ImageUrl}");
                        }
                    }

                    // إدخال الصور باستخدام المعرفات الصحيحة
                    await DbContext.Set<PlantImages>().AddRangeAsync(plantImages);
                    await DbContext.SaveChangesAsync();
                    Console.WriteLine("Plant_Images data seeded successfully!");
                }
                #endregion

                #region PlantAdditionalData data seeding
                string plantDataFilePath = Path.Combine(baseDir, "Data", "DataSeeding", "plant_data.json");
                Console.WriteLine($"Looking for plant_data.json at: {plantDataFilePath}");

                if (!File.Exists(plantDataFilePath))
                {
                    throw new FileNotFoundException($"Could not find seeding file at path: {plantDataFilePath}");
                }

                var plantDataJson = File.ReadAllText(plantDataFilePath);
                var additionalDataList = JsonSerializer.Deserialize<List<PlantAdditionalData>>(plantDataJson);

                if (additionalDataList?.Count > 0)
                {
                    Console.WriteLine($"Seeding {additionalDataList.Count} PlantAdditionalData records...");
                    await DbContext.PlantAdditionalData.AddRangeAsync(additionalDataList);
                    await DbContext.SaveChangesAsync();
                    Console.WriteLine("PlantAdditionalData data seeded successfully!");
                }

                #endregion

                #region Plant-Name-DATA seeding

                string plantNameDataFilePath = Path.Combine(baseDir, "Data", "DataSeeding", "Plant_Name_DATA.json");
                Console.WriteLine($"Looking for plant_name_data.json at: {plantNameDataFilePath}");

                if (!File.Exists(plantNameDataFilePath))
                {
                    throw new FileNotFoundException($"Could not find seeding file at path: {plantNameDataFilePath}");
                }

                var plantNameDataJson = File.ReadAllText(plantNameDataFilePath);
                var plantList = JsonSerializer.Deserialize<List<Plant>>(plantNameDataJson);

                if (plantList?.Count > 0)
                {
                    Console.WriteLine($"Seeding {plantList.Count} Plant records...");

                    // Remove existing Plant data if any
                    DbContext.Set<Plant>().RemoveRange(DbContext.Set<Plant>());
                    await DbContext.SaveChangesAsync();

                    // Process the list to set the JSON properties
                    foreach (var plant in plantList)
                    {
                        // Serialize the lists to JSON strings for database storage
                        plant.VitaminsJson = JsonSerializer.Serialize(plant.Vitamins);
                        plant.HealthBenefitsJson = JsonSerializer.Serialize(plant.HealthBenefits);
                    }

                    // Add new Plant data
                    await DbContext.Set<Plant>().AddRangeAsync(plantList);
                    await DbContext.SaveChangesAsync();
                    Console.WriteLine("Plant-Name-DATA seeded successfully!");
                }
                else
                {
                    Console.WriteLine("No Plant records found in the JSON file.");
                }
                #endregion


            }
            catch (Exception ex)
            {
                throw new Exception($"Error during data seeding: {ex.Message}", ex);
            }
        }

    }
}