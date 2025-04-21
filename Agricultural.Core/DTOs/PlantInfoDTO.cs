namespace Agricultural.DTOs
{
    public class PlantInfoDTO
    {
        public string PlantName { get; set; }
        public string SeasonType { get; set; }
        public string SoilType { get; set; }
        public double TemperatureMin { get; set; }
        public double TemperatureMax { get; set; }
        public double HumidityMin { get; set; }
        public double HumidityMax { get; set; }
        public string WaterNeeds { get; set; }
        public string SunlightNeeds { get; set; }
        public string GrowthTime { get; set; }
        public string FertilizerType { get; set; }
        public string CommonDiseases { get; set; }
        public string PestControl { get; set; }
        public string Uses { get; set; }
        public List<PlantImageDTO> Images { get; set; }
    }
}
