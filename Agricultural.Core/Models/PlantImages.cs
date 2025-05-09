

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
