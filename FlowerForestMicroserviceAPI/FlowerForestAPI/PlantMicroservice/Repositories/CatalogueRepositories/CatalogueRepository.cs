using PlantMicroservice.DbContexts;
using PlantMicroservice.Models;
using Microsoft.EntityFrameworkCore;
using ResponseUtility.ResponseServices;
using ResponseUtility.ResponseServices.Models;

namespace PlantMicroservice.Repositories.CatalogueRepositories
{
    public class CatalogueRepository : ICatalogueRepository
    {
        private readonly PlantDbContext plantDbContext;
        private readonly IResponseService responseService;

        public CatalogueRepository(PlantDbContext plantDbContext, IResponseService responseService)
        {
            this.plantDbContext = plantDbContext;
            this.responseService = responseService;
        }

        public async Task<Response> AddCatalogue(Catalogue catalogue)
        {
            await plantDbContext.Catalogues.AddAsync(catalogue);
            var result = await plantDbContext.SaveChangesAsync();

            var message = result == 0?
                Messages.ERROR_POST_INTERNAL :
                Messages.SUCCESS_POST_CREATED;

            return responseService.CreateResponse(message, result);
        }

        public async Task<Response> DeleteCatalogueById(Guid id)
        {
            var catalogue = await plantDbContext.Catalogues
                .SingleOrDefaultAsync(u => u.Id == id);

            var message = Messages.SUCCESS_DELETE_DELETED;


            if (catalogue != null)
            {
                message = Messages.INFORMATION_DELETE_NOTFOUND;
                plantDbContext.Catalogues.Remove(catalogue);
            }

            var result = await plantDbContext.SaveChangesAsync();

            return responseService.CreateResponse(message, result);
        }

        public async Task<Response> DeleteCatalogue(Catalogue catalogue)
        {
            plantDbContext.Catalogues.Remove(catalogue);

            var result = await plantDbContext.SaveChangesAsync();

            var message = result == 0 ?
                Messages.ERROR_DELETE_INVALIDREQUEST :
                Messages.SUCCESS_DELETE_DELETED;

            return responseService.CreateResponse(message, result);
        }

        public async Task<Response> GetCatalogueById(Guid id)
        {
            var user = await plantDbContext.Catalogues
                .SingleOrDefaultAsync(u => u.Id == id);

            var message = user == null ?
                Messages.INFORMATION_GET_NOTFOUND :
                Messages.SUCCESS_GET_RETRIEVED;

            return responseService.CreateResponse(message, user);
        }

        public async Task<Response> GetCatalogues()
        {
            var users = await plantDbContext.Catalogues
                .ToListAsync();

            var message = users.Count() > 0 ?
                Messages.INFORMATION_GET_NOTFOUND :
                Messages.SUCCESS_GET_RETRIEVED;

            return responseService.CreateResponse(message, users);
        }

        public async Task<Response> UpdateCatalogue(Catalogue catalogue)
        {
            plantDbContext.Catalogues.Update(catalogue);
            var result = await plantDbContext.SaveChangesAsync();

            var message = result == 0 ?
                Messages.ERROR_POST_INTERNAL :
                Messages.SUCCESS_POST_CREATED;

            return responseService.CreateResponse(message, result);
        }
    }
}
