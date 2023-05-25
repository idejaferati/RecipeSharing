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
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // ignore omitted parameters on models to enable optional params
    x.JsonSerializerOptions.IgnoreNullValues = true;
});
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




//builder.Services.AddAuthorization(options =>
//{
//    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
//    {

//        List<string> GetRoles(string nameOfPolicy)
//        {
//            var context = scope.ServiceProvider.GetService<RecipeSharingDbContext>();
//            var roleIds = context.PolicyRoles
//                                             .Where(rp => context.Policies
//                                                                    .Where(p => p.Name == nameOfPolicy)
//                                                                    .Select(p => p.Id)
//                                                                    .Contains(rp.PolicyId))
//                                             .Select(rp => rp.RoleId)
//                                             .ToList();

//            List<string> roleStrings = roleIds.ConvertAll(roleId => roleId.ToString());
//            return roleStrings;
//        }

//        try
//        {

//            options.AddPolicy("adminPolicy", policy =>
//                    policy.RequireRole(GetRoles("adminPolicy")));
//            options.AddPolicy("userPolicy", policy =>
//                    policy.RequireRole(GetRoles("userPolicy")));
//            options.AddPolicy("editorPolicy", policy =>
//                    policy.RequireRole(GetRoles("editorPolicy")));
//        }
//        catch (Exception ex) { }

//    }
//});


var mapperConfiguration = new MapperConfiguration(
    mc => mc.AddProfile(new AutoMapperConfigurations()));
IMapper mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddServices();

var app = builder.Build();


app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000", "http://192.168.139.1:3000"));

app.Use(async (context, next) =>
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetService<RecipeSharingDbContext>();
        var authorizationOptions = scope.ServiceProvider.GetRequiredService<IOptions<AuthorizationOptions>>().Value;
        // Create and configure the DynamicAuthorizationMiddleware instance
        var dynamicAuthorizationMiddleware = new DynamicAuthorizationMiddleware(next, dbContext, authorizationOptions);
        await dynamicAuthorizationMiddleware.InvokeAsync(context);
    }
});
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