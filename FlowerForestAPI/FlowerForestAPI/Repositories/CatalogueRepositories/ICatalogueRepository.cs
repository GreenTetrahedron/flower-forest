using FlowerForestAPI.Models;
using FlowerForestAPI.ResponseServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Repositories.CatalogueRepositories
{
    public interface ICatalogueRepository
    {
        dynamic GetColumnByCatalogueId(string column, Guid id);
        Response GetCataloguesByUserId(Guid userId);
        Response GetCatalogueById(Guid id);
        Response GetCatalogues();
        Response AddCatalogue(Catalogue catalogue);
        Response UpdateCatalogue(Catalogue catalogue);
        Response DeleteCatalogue(Catalogue catalogue);
    }
}
