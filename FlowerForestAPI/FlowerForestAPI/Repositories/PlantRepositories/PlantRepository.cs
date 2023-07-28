using FlowerForestAPI.DbContexts;
using FlowerForestAPI.DTOs;
using FlowerForestAPI.Models;
using FlowerForestAPI.ResponseServices;
using FlowerForestAPI.ResponseServices.Models;
using System;
using System.Linq;

namespace FlowerForestAPI.Repositories.PlantRepositories
{
    public class PlantRepository : IPlantRepository
    {
        private readonly FlowerForestContext flowerForestContext;

        private readonly IResponseService responseService;

        public PlantRepository(FlowerForestContext flowerForestContext, IResponseService responseService)
        {
            this.flowerForestContext = flowerForestContext;
            this.responseService = responseService;
        }

        private static PlantDTO PlantToPlantDTO(Plant plant)
        {
            if (plant == null)
                return null;

            return new PlantDTO
            {
                Id = plant.Id,
                Genus = plant.Genus,
                Species = plant.Species,
                PhotoUrl = plant.PhotoUrl,
                CommonName = plant.CommonName,
                MaxHeight_metres = plant.MaxHeight_metres,
                CatalogueId = plant.CatalogueId           
            };
        }

        public Response DeletePlant(Plant plant)
        {
            flowerForestContext.Plants.Remove(plant);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.INFORMATION_DELETE_NOTFOUND : Messages.SUCCESS_DELETE_DELETED;

            return responseService.CreateResponse(message, result);
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

            return responseService.CreateResponse(message, responsePlant);
        }

        public Response GetPlantsByCatalogueId(Guid id)
        {
            var plants = flowerForestContext.Plants
                .Where(p => p.CatalogueId == id)
                .Select(p => PlantToPlantDTO(p));

            var message = plants.Count() > 0 ?
                Messages.SUCCESS_GET_RETRIEVED : Messages.INFORMATION_GET_NOTFOUND;

            return responseService.CreateResponse(message, plants);
        }

        public Response GetPlants()
        {
            var plants = flowerForestContext.Plants
                .Select(p => PlantToPlantDTO(p))
                .ToList();

            var message = plants.Count() > 0 ?
                Messages.SUCCESS_GET_RETRIEVED : Messages.INFORMATION_GET_NOTFOUND;

            return responseService.CreateResponse(message, plants);
        }

        public Response AddPlant(Plant plant)
        {
            flowerForestContext.Plants.Add(plant);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.ERROR_POST_INTERNAL : Messages.SUCCESS_POST_CREATED;

            PlantDTO response = null;

            if (result != 0)
            {
                message = Messages.SUCCESS_POST_CREATED;
                response = PlantToPlantDTO(plant);                
            }

            return responseService.CreateResponse(message, response);
        }

        public Response UpdatePlant(Plant plant)
        {
            flowerForestContext.Plants.Update(plant);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.INFORMATION_PUT_NOTFOUND : Messages.SUCCESS_PUT_UPDATED;

            return responseService.CreateResponse(message, result);
        }
    }
}
