using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Agricultural.Core.Models
{
    public class PlantsInfo : BaseEntity
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

        //FK
        public ICollection<PlantImages> PlantImages{ get; set; } = new List<PlantImages>();


    }
}
