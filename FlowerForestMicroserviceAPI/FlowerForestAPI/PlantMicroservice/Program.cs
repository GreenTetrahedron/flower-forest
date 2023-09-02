using IdentityUtility.Authorization.Policies.Handlers;
using IdentityUtility.Authorization.Policies.Requirements;
using MessageBrokerClient.MessageReceiverServices;
using MessageBrokerClient.Models.Exchanges;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlantMicroservice.DbContexts;
using PlantMicroservice.Models;
using PlantMicroservice.Repositories.CatalogueRepositories;
using PlantMicroservice.Repositories.PlantRepositories;
using ResponseUtility.ResponseServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IAuthorizationHandler, SameAuthorHandler<Plant>>();
builder.Services.AddSingleton<IAuthorizationHandler, SameAuthorHandler<Catalogue>>();
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy("SamePlantAuthorPolicy",
        policy =>
        {
            policy.RequireClaim("UserId");

            policy.Requirements.Add(new SameAuthorRequirement<Plant>(
                    (plant, userId) => plant.Catalogue.UserId.ToString() == userId,
                    "UserId"
                )); 
        });

    options.AddPolicy("SameCatalogueAuthorPolicy",
        policy =>
        {
            policy.RequireClaim("UserId");

            policy.Requirements.Add(new SameAuthorRequirement<Catalogue>(
                    (catalogue, userId) => catalogue.UserId.ToString() == userId,
                    "UserId"
                ));
        });
});

string policyName = "UseAllowedRequestOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName, policy =>
    {
        string[] allowedHosts = builder.Configuration.GetSection("AllowedHosts").Get<string[]>();
        policy.WithOrigins(allowedHosts).AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PlantDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<IMessageReceiverService, RabbitMQMessageReceiverService>();

builder.Services.AddScoped<IPlantRepository, PlantRepository>();
builder.Services.AddScoped<ICatalogueRepository, CatalogueRepository>();

builder.Services.AddScoped<IResponseService, ResponseService>();

var app = builder.Build();

var messageReceiverService = app.Services.GetService<IMessageReceiverService>();

using var scope = app.Services.CreateScope();
var catalogueRepository = scope.ServiceProvider.GetService<ICatalogueRepository>();
var exchange = MessageBrokerExchanges.Exchanges[MessageBrokerExchangeNames.Catalogue];

messageReceiverService.SubscribeToQueue(
    routingKey: "add-catalogue",
    exchange: exchange,
    onReceiveDo: async (byte[] body) =>
    {
        await catalogueRepository.AddCatalogue(JsonConvert.DeserializeObject<Catalogue>(Encoding.UTF8.GetString(body)));
    }
);
messageReceiverService.SubscribeToQueue(
    routingKey: "update-catalogue",
    exchange: exchange,
    onReceiveDo: async (byte[] body) =>
    {
        await catalogueRepository.UpdateCatalogue(JsonConvert.DeserializeObject<Catalogue>(Encoding.UTF8.GetString(body)));
    }
);
messageReceiverService.SubscribeToQueue(
    routingKey: "delete-catalogue",
    exchange: exchange,
    onReceiveDo: async (byte[] body) =>
    {
        await catalogueRepository.DeleteCatalogue(JsonConvert.DeserializeObject<Catalogue>(Encoding.UTF8.GetString(body)));
    }
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
