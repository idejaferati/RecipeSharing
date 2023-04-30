using Microsoft.EntityFrameworkCore;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.DataLayer.Data;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;
using RecipeSharingApi.Data.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Data.Repository.IRepository;
using RecipeSharingApi.DataLayer.Models.Entities;
using RecipeSharingApi.DataLayer.Data.Repository;
using AutoMapper;
using RecipeSharingApi.BusinessLogic.Helpers;
using Microsoft.OpenApi.Models;


using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<RecipeSharingDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("RecipeSharingConnection")));
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
});

var mapperConfiguration = new MapperConfiguration(
    mc => mc.AddProfile(new AutoMapperConfigurations()));
IMapper mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddServices();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
// ASP .NET 