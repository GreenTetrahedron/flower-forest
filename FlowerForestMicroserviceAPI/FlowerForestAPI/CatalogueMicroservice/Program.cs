using CatalogueMicroservice.DbContexts;
using CatalogueMicroservice.Models;
using CatalogueMicroservice.Repositories.CatalogueRepositories;
using CatalogueMicroservice.Repositories.UserRepositories;
using ResponseUtility.ResponseServices;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using MessageBrokerClient.MessageReceiverServices;
using MessageBrokerClient.Models.Exchanges;
using MessageBrokerClient.MessageSenderServices;
using Microsoft.AspNetCore.Authorization;
using IdentityUtility.Authorization.Policies.Handlers;
using IdentityUtility.Authorization.Policies.Requirements;
using CatalogueMicroservice.Authentication.Requirements;
using CatalogueMicroservice.Authentication.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IAuthorizationHandler, SameAuthorHandler<Catalogue>>();
builder.Services.AddSingleton<IAuthorizationHandler, CatalogueIsPublicHandler>();
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy("SameCatalogueAuthorOrCatalogueIsPublic",
        policy =>
        {
            policy.Requirements.Add(new CatalogueIsPublicRequirement());

            policy.RequireClaim("UserId");

            policy.Requirements.Add(new SameAuthorRequirement<Catalogue>(
                    (catalogue, userId) => catalogue.UserId.ToString() == userId,
                    "UserId"
                ));
        });

    options.AddPolicy("SameCatalogueAuthor",
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

builder.Services.AddDbContext<CatalogueDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<IMessageReceiverService, RabbitMQMessageReceiverService>();
builder.Services.AddSingleton<IMessageSenderService, RabbitMQMessageSenderService>();
builder.Services.AddSingleton<IResponseService, ResponseService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICatalogueRepository, CatalogueRepository>();


var app = builder.Build();

var messageReceiverService = app.Services.GetService<IMessageReceiverService>();

using var scope = app.Services.CreateScope();
var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
var exchange = MessageBrokerExchanges.Exchanges[MessageBrokerExchangeNames.User];

messageReceiverService.SubscribeToQueue(
    routingKey: "add-user",
    exchange: exchange,
    onReceiveDo: async (byte[] body) =>
        {
            await userRepository.AddUser(JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(body)));
        }
);
messageReceiverService.SubscribeToQueue(
    routingKey: "update-user",
    exchange: exchange,
    onReceiveDo: async (byte[] body) =>
    {
        await userRepository.UpdateUser(JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(body)));
    }
);
messageReceiverService.SubscribeToQueue(
    routingKey: "delete-user",
    exchange: exchange,
    onReceiveDo: async (byte[] body) =>
    {
        await userRepository.DeleteUser(JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(body)));
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
