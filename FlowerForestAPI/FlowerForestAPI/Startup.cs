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
using FlowerForestAPI.ResponseServices;
using FlowerForestAPI.Repositories.CatalogueRepositories;
using FlowerForestAPI.Repositories.PlantRepositories;
using FlowerForestAPI.Repositories.UserRepositories;
using FlowerForestAPI.TokenServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FlowerForestAPI.Requirements;
using FlowerForestAPI.Requirements.SameUserAuthorizationHandler;
using Microsoft.AspNetCore.Authorization;
using FlowerForestAPI.AuthorizeUserServices;

namespace FlowerForestAPI
{
    public class Startup
    {
        private readonly string _policyName = "policyName";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt => {
                opt.AddPolicy(name: _policyName, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer( options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JWTConfiguration:Issuer"],
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWTConfiguration:Secret"]))
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CreatorOnlyPolicy", policy =>
                    policy.Requirements.Add(new SameUserRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, SameUserAuthorizationHandler>();

            services.AddDbContext<FlowerForestContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("FlowerForestDBConnectionString")));

            services.AddScoped<IResponseService, ResponseService>();
            services.AddScoped<IAuthorizeUserService, AuthorizeUserService>();
            services.AddScoped<ITokenService, JWTTokenService>();

            services.AddScoped<ICatalogueRepository, CatalogueRepository>();
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

            app.UseCors(_policyName);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
