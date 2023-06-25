using FlowerForestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class FlowerForestController
    {
        private FlowerForestContext flowerForestContext;

        public FlowerForestController(FlowerForestContext flowerForestContext)
        {
            this.flowerForestContext = flowerForestContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Plant>>> GetPlants()
        {
            return await flowerForestContext.Plants
                .Select(x => x).ToListAsync();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Plant>> GetPlantById(Guid id)
        {
            var plant = await flowerForestContext.Plants
                .FindAsync(id);

            if (plant == null)
                return new NotFoundResult();
           
            return plant;
        }
    }
}
