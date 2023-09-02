using PlantMicroservice.Models;
using ResponseUtility.ResponseServices.Models;

namespace PlantMicroservice.Repositories.CatalogueRepositories
{
    public interface ICatalogueRepository
    {
        Task<Response> GetCatalogueById(Guid id);
        Task<Response> GetCatalogues();
        Task<Response> AddCatalogue(Catalogue catalogue);
        Task<Response> UpdateCatalogue(Catalogue catalogue);
        Task<Response> DeleteCatalogueById(Guid id);
        Task<Response> DeleteCatalogue(Catalogue catalogue);
    }
}
