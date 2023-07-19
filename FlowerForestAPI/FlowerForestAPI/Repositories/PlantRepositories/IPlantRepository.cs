using FlowerForestAPI.Models;
using FlowerForestAPI.ResponseServices.Models;
using System;

namespace FlowerForestAPI.Repositories.PlantRepositories
{
    public interface IPlantRepository
    {
        Response GetPlants();
        Response GetPlantById(Guid id);
        Response GetPlantsByCatalogueId(Guid id);
        Response AddPlant(Plant plant);
        Response UpdatePlant(Plant plant);
        Response DeletePlant(Plant plant);
    }
}
