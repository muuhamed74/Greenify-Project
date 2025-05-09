
namespace Agricultural.Core.DTOs
{
    public class PlantDTO
    {
        public string Name { get; set; }
        public string ScientificName { get; set; }
        public GrowingConditionsDTO GrowingConditions { get; set; }
        public FlowerDTO Flower { get; set; }
        public List<string> Vitamins { get; set; }
        public List<string> HealthBenefits { get; set; }
    }

    public class GrowingConditionsDTO
    {
        public string Sunlight { get; set; }
        public string Water { get; set; }
        public string SoilType { get; set; }
    }

    public class FlowerDTO
    {
        public string Color { get; set; }
        public string Morphology { get; set; }
    }
}
