

namespace Agricultural.Core.Models
{
    public class PlantAdditionalData : BaseEntity
    {
            public string PlantName { get; set; }
            public string Status { get; set; }
            public string Description { get; set; }
            public string? Treatment { get; set; } // nullable
            public string? AdditionalInformation { get; set; } // nullable
        
    }


}
