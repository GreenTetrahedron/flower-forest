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
        Response AddCataloguedPlant(CataloguedPlant plant);
        Response UpdateCataloguedPlant(CataloguedPlant plant);
        Response DeleteCataloguedPlant(CataloguedPlant plant);
    }
}
