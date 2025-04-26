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
        [Required]
        [MaxLength(100)]
        public string PlantName { get; set; }

        [Required]
        [MaxLength(100)]
        public string ScientificName { get; set; }

        [Required]
        [MaxLength(50)]
        public string CareLevel { get; set; }

        [Required]
        [MaxLength(50)]
        public string Size { get; set; }

        [Required]
        [MaxLength(50)]
        public string Edibility { get; set; }

        public bool Flowering { get; set; }
        public bool Medicinal { get; set; }
        public bool IsAirPurifying { get; set; }

        [Required]
        public string About { get; set; }

        public PlantDetails Details { get; set; }

        //FK
        public ICollection<PlantImages> PlantImages { get; set; } = new List<PlantImages>();
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
