using FlowerForestAPI.DbContexts;
using FlowerForestAPI.DTOs;
using FlowerForestAPI.Models;
using FlowerForestAPI.ResponseServices;
using FlowerForestAPI.ResponseServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Repositories.CatalogueRepositories
{
    public class CatalogueRepository : ICatalogueRepository
    {
        private readonly FlowerForestContext flowerForestContext;
        private readonly IResponseService responseService;

        public CatalogueRepository(FlowerForestContext flowerForestContext, IResponseService responseService)
        {
            this.flowerForestContext = flowerForestContext;
            this.responseService = responseService;
        }

        private static CatalogueDTO CatalogueToCatalogueDTO(Catalogue catalogue)
        {
            if (catalogue == null)
                return null;

            return new CatalogueDTO
            {
                Id = catalogue.Id,
                Name = catalogue.Name,
                UserId = catalogue.UserId
            };
        }

        public Response DeleteCatalogue(Catalogue catalogue)
        {
            flowerForestContext.Catalogues.Remove(catalogue);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.INFORMATION_DELETE_NOTFOUND : Messages.SUCCESS_DELETE_DELETED;

            return responseService.CreateResponse(message, result);
        }

        public Response GetCatalogues()
        {
            var catalogues = flowerForestContext.Catalogues
                .Select(c => CatalogueToCatalogueDTO(c));

            var message = catalogues.Count() > 0 ?
                Messages.SUCCESS_GET_RETRIEVED : Messages.INFORMATION_GET_NOTFOUND;

            return responseService.CreateResponse(message, catalogues);
        }

        public Response GetCatalogueById(Guid id)
        {
            var catalogue = CatalogueToCatalogueDTO(
                flowerForestContext.Catalogues
                .SingleOrDefault(c => c.Id == id));

            var message = catalogue == null ?
                Messages.INFORMATION_GET_NOTFOUND : Messages.SUCCESS_GET_RETRIEVED;

            return responseService.CreateResponse(message, catalogue);
        }

        public Response GetCataloguesByUserId(Guid userId)
        {
            var catalogues = flowerForestContext.Catalogues
                .Where(c => c.UserId == userId)
                .Select(c => CatalogueToCatalogueDTO(c));

            var message = catalogues.Count() == 0 ?
                Messages.INFORMATION_GET_NOTFOUND : Messages.SUCCESS_GET_RETRIEVED;

            return responseService.CreateResponse(message, catalogues);
        }

        public Response AddCatalogue(Catalogue catalogue)
        {
            flowerForestContext.Catalogues.Add(catalogue);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.ERROR_POST_INTERNAL : Messages.SUCCESS_POST_CREATED;

            return responseService.CreateResponse(message, result);
        }

        public Response UpdateCatalogue(Catalogue catalogue)
        {
            flowerForestContext.Catalogues.Update(catalogue);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.INFORMATION_PUT_NOTFOUND : Messages.SUCCESS_PUT_UPDATED;

            return responseService.CreateResponse(message, result);
        }
    }
}
