using FlowerForestAPI.DbContexts;
using FlowerForestAPI.DTOs;
using FlowerForestAPI.Models;
using FlowerForestAPI.ResponseHandlers;
using FlowerForestAPI.ResponseHandlers.Models;
using System;
using System.Linq;

namespace FlowerForestAPI.Repositories.PlantRepositories
{
    public class PlantRepository : IPlantRepository
    {
        private readonly FlowerForestContext flowerForestContext;

        private readonly IResponseHandler responseHandler;

        public PlantRepository(FlowerForestContext flowerForestContext, IResponseHandler responseHandler)
        {
            this.flowerForestContext = flowerForestContext;
            this.responseHandler = responseHandler;
        }

        private static PlantDTO PlantToPlantDTO(Plant plant)
        {
            return new PlantDTO
            {
                Id = plant.Id,
                Genus = plant.Genus,
                Species = plant.Species,
                PhotoUrl = plant.PhotoUrl,
                CommonName = plant.CommonName,
                MaxHeight_metres = plant.MaxHeight_metres
            };
        }

        public Response DeletePlant(Plant plant)
        {
            flowerForestContext.Plants.Remove(plant);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.INFORMATION_DELETE_NOTFOUND : Messages.SUCCESS_DELETE_DELETED;

            return responseHandler.CreateResponse(message, result);
        }

        public Response GetPlantById(Guid id)
        {
            var plant = flowerForestContext.Plants
                .SingleOrDefault(p => p.Id == id);

            var message = Messages.INFORMATION_GET_NOTFOUND;

            PlantDTO? responsePlant = null;

            if (plant != null)
            {
                message = Messages.SUCCESS_GET_RETRIEVED;

                responsePlant = PlantToPlantDTO(plant);
            }

            return responseHandler.CreateResponse(message, responsePlant);
        }

        public Response GetPlants()
        {
            var plants = flowerForestContext.Plants
                .Select(p => PlantToPlantDTO(p))
                .ToList();

            var message = plants.Count() > 0 ?
                Messages.SUCCESS_GET_RETRIEVED : Messages.INFORMATION_GET_NOTFOUND;

            return responseHandler.CreateResponse(message, plants);
        }

        public Response AddPlant(Plant plant)
        {
            flowerForestContext.Plants.Add(plant);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.ERROR_POST_INTERNAL : Messages.SUCCESS_POST_CREATED;

            return responseHandler.CreateResponse(message, result);
        }

        public Response UpdatePlant(Plant plant)
        {
            flowerForestContext.Plants.Update(plant);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.INFORMATION_PUT_NOTFOUND : Messages.SUCCESS_PUT_UPDATED;

            return responseHandler.CreateResponse(message, result);
        }
    }
}
