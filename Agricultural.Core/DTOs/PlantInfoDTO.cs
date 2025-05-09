using Agricultural.Core.DTOs;

namespace Agricultural.DTOs
{
    public class PlantInfoDTO
    {
         public string PlantName { get; set; }

        
        public string ScientificName { get; set; }

       
        public string CareLevel { get; set; }

       
        public string Size { get; set; }

        
        public string Edibility { get; set; }

        public bool Flowering { get; set; }
        public bool Medicinal { get; set; }
        public bool IsAirPurifying { get; set; }

        
        public string About { get; set; }

        public PlantDetails Details { get; set; }

        public List<PlantImageDTO> Images { get; set; }


    }

    public class PlantDetails
    {
        public string Temperature { get; set; }
        public string Sunlight { get; set; }
        public string Water { get; set; }
        public string Repotting { get; set; }
        public string Fertilizing { get; set; }
        public string Pests { get; set; }
    }
    
}
