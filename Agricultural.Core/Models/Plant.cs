using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Agricultural.Core.Models
{
    public class Plant : BaseEntity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("scientific_name")]
        public string ScientificName { get; set; }
        
        [JsonPropertyName("growing_conditions")]
        public GrowingConditions GrowingConditions { get; set; }
        
        [JsonPropertyName("flower")]
        public Flower Flower { get; set; }
        
        // Store JSON strings in the database
        public string VitaminsJson { get; set; }
        public string HealthBenefitsJson { get; set; }
        
        // These properties are for JSON deserialization only
        [NotMapped]
        [JsonPropertyName("vitamins")]
        public List<string> Vitamins { get; set; }
        
        [NotMapped]
        [JsonPropertyName("health_benefits")]
        public List<string> HealthBenefits { get; set; }
    }

    [Owned]
    public class GrowingConditions
    {
        [JsonPropertyName("sunlight")]
        public string Sunlight { get; set; }
        
        [JsonPropertyName("water")]
        public string Water { get; set; }
        
        [JsonPropertyName("soil_type")]
        public string SoilType { get; set; }
    }

    [Owned]
    public class Flower
    {
        [JsonPropertyName("color")]
        public string Color { get; set; }
        
        [JsonPropertyName("morphology")]
        public string Morphology { get; set; }
    }
}
