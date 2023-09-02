using Microsoft.EntityFrameworkCore;
using PlantMicroservice.DbContexts;
using PlantMicroservice.Models;
using PlantMicroservice.Models.DTOs;
using ResponseUtility.ResponseServices;
using ResponseUtility.ResponseServices.Models;

namespace PlantMicroservice.Repositories.PlantRepositories
{
    public class PlantRepository : IPlantRepository
    {
        private readonly PlantDbContext plantDbContext;

        private readonly IResponseService responseService;

        public PlantRepository(PlantDbContext plantDbContext, IResponseService responseService)
        {
            this.plantDbContext = plantDbContext;
            this.responseService = responseService;
        }

        private static PlantDTO PlantToPlantDTO(Plant plant)
        {
            if (plant == null)
                throw new ArgumentNullException("'plant' was null");

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

        public async Task<Response> GetPlantById(Guid id)
        {
            var plant = await plantDbContext.Plants
                .SingleOrDefaultAsync(p => p.Id == id);

            var message = Messages.INFORMATION_GET_NOTFOUND;

            PlantDTO? responsePlant = null;

            if (plant != null)
            {
                message = Messages.SUCCESS_GET_RETRIEVED;

                responsePlant = PlantToPlantDTO(plant);
            }

            return responseService.CreateResponse(message, responsePlant);
        }

        public async Task<Response> GetPlantsByCatalogueId(Guid id)
        {
            var plants = await plantDbContext.Plants
                .Where(p => p.CatalogueId == id)
                .Select(p => PlantToPlantDTO(p))
                .ToListAsync();

            var message = plants.Count() > 0 ?
                Messages.SUCCESS_GET_RETRIEVED : Messages.INFORMATION_GET_NOTFOUND;

            return responseService.CreateResponse(message, plants);
        }

        public async Task<Response> GetPlants()
        {
            var plants = await plantDbContext.Plants
                .Select(p => PlantToPlantDTO(p))
                .ToListAsync();

            var message = plants.Count() > 0 ?
                Messages.SUCCESS_GET_RETRIEVED : Messages.INFORMATION_GET_NOTFOUND;

            return responseService.CreateResponse(message, plants);
        }

        public async Task<Response> AddPlant(Plant plant)
        {
            await plantDbContext.Plants.AddAsync(plant);
            var result = await plantDbContext.SaveChangesAsync();

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

        public async Task<Response> UpdatePlant(Plant plant)
        {
            plantDbContext.Plants.Update(plant);
            var result = await plantDbContext.SaveChangesAsync();

            var message = result == 0 ?
                Messages.INFORMATION_PUT_NOTFOUND : Messages.SUCCESS_PUT_UPDATED;

            return responseService.CreateResponse(message, result);
        }

        public async Task<Response> DeletePlantById(Guid id)
        {
            var plant = plantDbContext.Plants
                .SingleOrDefault(p => p.Id == id);

            var result = await plantDbContext.SaveChangesAsync();

            var message = result == 0 ?
                Messages.INFORMATION_DELETE_NOTFOUND : Messages.SUCCESS_DELETE_DELETED;

            return responseService.CreateResponse(message, result);
        }
    }
}
