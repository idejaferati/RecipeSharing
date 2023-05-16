using Microsoft.EntityFrameworkCore;
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
using Microsoft.EntityFrameworkCore.Metadata;

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



builder.Services.AddAuthorization(options =>
{
    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
    {

        List<string> GetRoles(string nameOfPolicy)
        {
            var context = scope.ServiceProvider.GetService<RecipeSharingDbContext>();
            var roleIds = context.PolicyRoles
                                             .Where(rp => context.Policies
                                                                    .Where(p => p.Name == nameOfPolicy)
                                                                    .Select(p => p.Id)
                                                                    .Contains(rp.PolicyId))
                                             .Select(rp => rp.RoleId)
                                             .ToList();

            List<string> roleStrings = roleIds.ConvertAll(roleId => roleId.ToString());
            return roleStrings;
        }


        //options.AddPolicy("onlyadmin", policy =>
        //        policy.RequireRole(GetRoles("onlyadmin")));
        options.AddPolicy("onlyadmin", policy =>
                policy.RequireRole(GetRoles("onlyadmin")));
        options.AddPolicy("getmydata", policy =>
                policy.RequireRole(GetRoles("getmydata")));
        //options.AddPolicy("onlyuser", policy =>
        //        policy.RequireRole(GetRoles("onlyuser")));

    }
});

var mapperConfiguration = new MapperConfiguration(
    mc => mc.AddProfile(new AutoMapperConfigurations()));
IMapper mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddServices();

var app = builder.Build();

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