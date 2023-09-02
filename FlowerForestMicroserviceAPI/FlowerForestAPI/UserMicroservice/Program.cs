using Microsoft.EntityFrameworkCore;
using MessageBrokerClient.MessageSenderServices;
using UserMicroservice.DbContexts;
using UserMicroservice.Repositories.UserRepositories;
using ResponseUtility.ResponseServices;
using UserMicroservice.AuthenticationService;
using Microsoft.AspNetCore.Authorization;
using UserMicroservice.Models;
using IdentityUtility.Authorization.Policies.Requirements;
using IdentityUtility.Authorization.Policies.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string policyName = "UseAllowedRequestOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName, policy =>
    {
        string[] allowedHosts = builder.Configuration.GetSection("AllowedHosts").Get<string[]>();
        policy.WithOrigins(allowedHosts).AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, SameAuthorHandler<User>>();

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy("SameAuthorPolicy",
        policy =>
        {
            policy.RequireClaim("UserId");
            policy.Requirements.Add(new SameAuthorRequirement<User>(
                    (User user, string id) => user.Id.ToString() == id,
                    "UserId"
                ));
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<IMessageSenderService, RabbitMQMessageSenderService>();

builder.Services.AddScoped<IResponseService, ResponseService>();

builder.Services.AddSingleton<ITokenService, JWTTokenService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

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
