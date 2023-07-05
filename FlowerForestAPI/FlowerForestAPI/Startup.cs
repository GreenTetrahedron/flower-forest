using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlowerForestAPI.Models;
using FlowerForestAPI.DbContexts;
using FlowerForestAPI.ResponseHandler;
using FlowerForestAPI.Repositories.CataloguedPlantRepositories;
using FlowerForestAPI.Repositories.PlantRepositories;
using FlowerForestAPI.Repositories.UserRepositories;

namespace FlowerForestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FlowerForestContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("FlowerForestDBConnectionString")));

            services.AddScoped<IResponseHandler, ResponseHandler.ResponseHandler>();

            services.AddScoped<ICataloguedPlantRepository, CataloguedPlantRepository>();
            services.AddScoped<IPlantRepository, PlantRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
