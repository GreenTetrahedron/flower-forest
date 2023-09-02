using CatalogueMicroservice.DbContexts;
using CatalogueMicroservice.Models;
using CatalogueMicroservice.Models.DTOs;
using MessageBrokerClient.MessageSenderServices;
using MessageBrokerClient.Models.Exchanges;
using Microsoft.EntityFrameworkCore;
using ResponseUtility.ResponseServices;
using ResponseUtility.ResponseServices.Models;

namespace CatalogueMicroservice.Repositories.CatalogueRepositories
{
    public class CatalogueRepository : ICatalogueRepository
    {
        private readonly CatalogueDbContext catalogueDbContext;
        private readonly IResponseService responseService;

        private IMessageSenderService messageSenderService;
        private AmqpExchange exchange;

        public CatalogueRepository(CatalogueDbContext catalogueDbContext, IResponseService responseService,
            IMessageSenderService messageSenderService)
        {
            this.catalogueDbContext = catalogueDbContext;
            this.responseService = responseService;

            this.messageSenderService = messageSenderService;

            exchange = MessageBrokerExchanges.Exchanges[MessageBrokerExchangeNames.Catalogue];
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

        public async Task<Response> DeleteCatalogueById(Guid id)
        {
            var catalogue = await catalogueDbContext.Catalogues
                .SingleOrDefaultAsync(c => c.Id == id);

            catalogueDbContext.Catalogues.Remove(catalogue);
            var result = await catalogueDbContext.SaveChangesAsync();

            var message = result == 0 ?
                Messages.INFORMATION_DELETE_NOTFOUND : Messages.SUCCESS_DELETE_DELETED;

            messageSenderService.SendData(
                CatalogueToCatalogueDTO(catalogue),
                "delete-catalogue",
                exchange
                );

            return responseService.CreateResponse(message, result);
        }

        public async Task<Response> GetCatalogues()
        {
            var catalogues = await catalogueDbContext.Catalogues
                .Select(c => CatalogueToCatalogueDTO(c))
                .ToListAsync();

            var message = catalogues.Count() > 0 ?
                Messages.SUCCESS_GET_RETRIEVED : Messages.INFORMATION_GET_NOTFOUND;

            return responseService.CreateResponse(message, catalogues);
        }

        public async Task<Response> GetCatalogueById(Guid id)
        {
            var catalogue = CatalogueToCatalogueDTO(
                await catalogueDbContext.Catalogues
                .SingleOrDefaultAsync(c => c.Id == id)
                );

            var message = catalogue == null ?
                Messages.INFORMATION_GET_NOTFOUND : Messages.SUCCESS_GET_RETRIEVED;

            return responseService.CreateResponse(message, catalogue);
        }

        public async Task<Response> GetCataloguesByUserId(Guid userId)
        {
            var catalogues = await catalogueDbContext.Catalogues
                .Where(c => c.UserId == userId)
                .Select(c => CatalogueToCatalogueDTO(c))
                .ToListAsync();

            var message = catalogues.Count() == 0 ?
                Messages.INFORMATION_GET_NOTFOUND : Messages.SUCCESS_GET_RETRIEVED;

            return responseService.CreateResponse(message, catalogues);
        }

        public async Task<Response> AddCatalogue(CatalogueDTO catalogue)
        {
            var catalogueToAdd = new Catalogue()
            {
                Name = catalogue.Name,
                UserId = catalogue.UserId
            };

            await catalogueDbContext.Catalogues.AddAsync(catalogueToAdd);
            var result = await catalogueDbContext.SaveChangesAsync();

            CatalogueDTO response = null;

            var message = Messages.ERROR_POST_INVALIDREQUEST;

            if (result != 0)
            {
                message = Messages.SUCCESS_POST_CREATED;
                response = CatalogueToCatalogueDTO(catalogueToAdd);

                messageSenderService.SendData(
                    response,
                    "add-catalogue",
                    exchange
                    );
            }

            return responseService.CreateResponse(message, response);
        }

        public async Task<Response> UpdateCatalogue(Catalogue catalogue)
        {
            catalogueDbContext.ChangeTracker.Clear();
            catalogueDbContext.Catalogues.Update(catalogue);
            var result = await catalogueDbContext.SaveChangesAsync();

            var message = result == 0 ?
                Messages.INFORMATION_PUT_NOTFOUND : Messages.SUCCESS_PUT_UPDATED;


            messageSenderService.SendData(
                CatalogueToCatalogueDTO(catalogue),
                "delete-catalogue",
                exchange
                );

            return responseService.CreateResponse(message, result);
        }

        public dynamic GetColumnByCatalogueId(string column, Guid id)
        {
            var catalogue = catalogueDbContext.Catalogues
                .SingleOrDefault(c => c.Id == id);

            var result = catalogue?.GetType()
                .GetProperty(column).GetValue(catalogue);

            return result;
        }

        public async Task<Response> GetPublicCatalogues()
        {
            var catalogues = await catalogueDbContext.Catalogues
                .Where(c => c.IsPublic == true)
                .Select(c => CatalogueToCatalogueDTO(c))
                .ToListAsync();

            var message = catalogues.Count() > 0 ?
                Messages.SUCCESS_GET_RETRIEVED : Messages.INFORMATION_GET_NOTFOUND;

            return responseService.CreateResponse(message, catalogues);
        }
    }
}
