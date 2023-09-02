using PlantMicroservice.Models;
using ResponseUtility.ResponseServices.Models;

namespace PlantMicroservice.Repositories.PlantRepositories
{
    public interface IPlantRepository
    {
        Task<Response> GetPlants();
        Task<Response> GetPlantById(Guid id);
        Task<Response> GetPlantsByCatalogueId(Guid id);
        Task<Response> AddPlant(Plant plant);
        Task<Response> UpdatePlant(Plant plant);
        Task<Response> DeletePlantById(Guid id);
    }
}
