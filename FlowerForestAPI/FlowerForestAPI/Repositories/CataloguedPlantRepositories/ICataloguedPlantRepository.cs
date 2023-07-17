using FlowerForestAPI.Models;
using FlowerForestAPI.ResponseHandlers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Repositories.CataloguedPlantRepositories
{
    public interface ICataloguedPlantRepository
    {
        Response GetCataloguedPlantsByUserId(Guid userId);
        Response GetCataloguedPlantById(Guid id);
        Response GetCataloguedPlants();
        Response AddCataloguedPlant(Catalogue plant);
        Response UpdateCataloguedPlant(Catalogue plant);
        Response DeleteCataloguedPlant(Catalogue plant);
    }
}
