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
                // إزالة البيانات الحالية لضمان نظافة البيانات
                DbContext.PlantImages.RemoveRange(DbContext.PlantImages);
                DbContext.PlantsInfo.RemoveRange(DbContext.PlantsInfo);
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
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during data seeding: {ex.Message}", ex);
            }
        }

    }
}