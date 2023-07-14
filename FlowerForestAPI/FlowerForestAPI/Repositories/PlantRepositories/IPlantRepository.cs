using FlowerForestAPI.Models;
using FlowerForestAPI.ResponseHandlers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Repositories.PlantRepositories
{
    public interface IPlantRepository
    {
        Response GetPlants();
        Response GetPlantById(Guid id);
        Response AddPlant(Plant plant);
        Response UpdatePlant(Plant plant);
        Response DeletePlant(Plant plant);
    }
}
