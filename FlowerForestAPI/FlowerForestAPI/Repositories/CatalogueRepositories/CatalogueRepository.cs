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
                IsPublic = catalogue.IsPublic,
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

        public Response AddCatalogue(CatalogueDTO catalogue)
        {
            var catalogueToAdd = new Catalogue()
            {
                Name = catalogue.Name,
                UserId = catalogue.UserId
            };

            flowerForestContext.Catalogues.Add(catalogueToAdd);
            var result = flowerForestContext.SaveChanges();

            CatalogueDTO response = null;

            var message = Messages.ERROR_POST_INVALIDREQUEST;

            if (result != 0)
            {
                message = Messages.SUCCESS_POST_CREATED;
                response = catalogue;
            }

            return responseService.CreateResponse(message, response);
        }

        public Response UpdateCatalogue(Catalogue catalogue)
        {
            flowerForestContext.ChangeTracker.Clear();
            flowerForestContext.Catalogues.Update(catalogue);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.INFORMATION_PUT_NOTFOUND : Messages.SUCCESS_PUT_UPDATED;

            return responseService.CreateResponse(message, result);
        }

        public dynamic GetColumnByCatalogueId(string column, Guid id)
        {
            var catalogue = flowerForestContext.Catalogues
                .SingleOrDefault(c => c.Id == id);

            var result = catalogue?.GetType()
                .GetProperty(column).GetValue(catalogue);

            return result;
        }

        public Response GetPublicCatalogues()
        {
            var catalogues = flowerForestContext.Catalogues
                .Where(c => c.IsPublic == true)
                .Select(c => CatalogueToCatalogueDTO(c));

            var message = catalogues.Count() > 0 ?
                Messages.SUCCESS_GET_RETRIEVED : Messages.INFORMATION_GET_NOTFOUND;

            return responseService.CreateResponse(message, catalogues);
        }
    }
}
