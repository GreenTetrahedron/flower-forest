using FlowerForestAPI.DbContexts;
using FlowerForestAPI.Models;
using FlowerForestAPI.ResponseHandlers;
using FlowerForestAPI.ResponseHandlers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Repositories.CataloguedPlantRepositories
{
    public class CataloguedPlantRepository : ICataloguedPlantRepository
    {
        private readonly FlowerForestContext flowerForestContext;
        private readonly IResponseHandler responseHandler;

        public CataloguedPlantRepository(FlowerForestContext flowerForestContext, IResponseHandler responseHandler)
        {
            this.flowerForestContext = flowerForestContext;
            this.responseHandler = responseHandler;
        }

        public Response DeleteCataloguedPlant(CataloguedPlant plant)
        {
            flowerForestContext.CataloguedPlants.Remove(plant);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.INFORMATION_DELETE_NOTFOUND : Messages.SUCCESS_DELETE_DELETED;

            return responseHandler.CreateResponse(message, result);
        }

        public Response GetCataloguedPlants()
        {
            var plants = flowerForestContext.CataloguedPlants;

            var message = plants.Count() > 0 ?
                Messages.SUCCESS_GET_RETRIEVED : Messages.INFORMATION_GET_NOTFOUND;

            return responseHandler.CreateResponse(message, plants);
        }

        public Response GetCataloguedPlantById(Guid id)
        {
            var plant = flowerForestContext.CataloguedPlants
                .SingleOrDefault(p => p.Id == id);

            var message = plant == null ?
                Messages.INFORMATION_GET_NOTFOUND : Messages.SUCCESS_GET_RETRIEVED;

            return responseHandler.CreateResponse(message, plant);
        }

        public Response GetCataloguedPlantsByUserId(Guid userId)
        {
            var plants = flowerForestContext.CataloguedPlants
                .Where(p => p.UserId == userId);

            var message = plants.Count() == 0 ?
                Messages.INFORMATION_GET_NOTFOUND : Messages.SUCCESS_GET_RETRIEVED;

            return responseHandler.CreateResponse(message, plants);
        }

        public Response AddCataloguedPlant(CataloguedPlant plant)
        {
            flowerForestContext.CataloguedPlants.Add(plant);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.ERROR_POST_INTERNAL : Messages.SUCCESS_POST_CREATED;

            return responseHandler.CreateResponse(message, result);
        }

        public Response UpdateCataloguedPlant(CataloguedPlant plant)
        {
            flowerForestContext.CataloguedPlants.Update(plant);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.INFORMATION_PUT_NOTFOUND : Messages.SUCCESS_PUT_UPDATED;

            return responseHandler.CreateResponse(message, result);
        }
    }
}
