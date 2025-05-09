using Newtonsoft.Json;

namespace Agricultural.Core.Models
{
    public class PlantNamePrediction
    {
        
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
