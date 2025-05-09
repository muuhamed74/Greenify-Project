using System.Threading.Tasks;


namespace Agricultural.Serv.Services
{
    public interface IPlantAdditionalDataService
    {
       
            Task<object> GetPlantDataAsync(string plantName, string status);
        

    }

}
