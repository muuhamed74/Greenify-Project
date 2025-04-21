using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agricultural.Core.Models
{
    public class PlantImages : BaseEntity
    {

      
        public string ImageUrl { get; set; }

        
        

        //FK
        public int PlantsInfoId { get; set; }

        public PlantsInfo PlantsInfo { get; set; }


       
           

           
        




    }
}
