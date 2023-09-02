using CatalogueMicroservice.Models;
using CatalogueMicroservice.Models.DTOs;
using ResponseUtility.ResponseServices.Models;

namespace CatalogueMicroservice.Repositories.CatalogueRepositories
{
    public interface ICatalogueRepository
    {
        dynamic GetColumnByCatalogueId(string column, Guid id);
        Task<Response> GetCataloguesByUserId(Guid userId);
        Task<Response> GetPublicCatalogues();
        Task<Response> GetCatalogueById(Guid id);
        Task<Response> GetCatalogues();
        Task<Response> AddCatalogue(CatalogueDTO catalogue);
        Task<Response> UpdateCatalogue(Catalogue catalogue);
        Task<Response> DeleteCatalogueById(Guid id);
    }
}
