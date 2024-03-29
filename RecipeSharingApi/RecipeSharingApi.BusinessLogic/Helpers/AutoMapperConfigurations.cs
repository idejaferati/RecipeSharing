using AutoMapper;
using RecipeSharingApi.DataLayer.Models.DTOs;
using RecipeSharingApi.DataLayer.Models.DTOs.Collection;
using RecipeSharingApi.DataLayer.Models.DTOs.CookBook;
using RecipeSharingApi.DataLayer.Models.DTOs.Cuisine;
using RecipeSharingApi.DataLayer.Models.DTOs.Ingredient;
using RecipeSharingApi.DataLayer.Models.DTOs.Instruction;
using RecipeSharingApi.DataLayer.Models.DTOs.Recipe;
using RecipeSharingApi.DataLayer.Models.DTOs.Review;
using RecipeSharingApi.DataLayer.Models.DTOs.ShoppingList;
using RecipeSharingApi.DataLayer.Models.DTOs.Tag;
using RecipeSharingApi.DataLayer.Models.DTOs.User;
using RecipeSharingApi.DataLayer.Models.Entities;
using RecipeSharingApi.DataLayer.Models.Entities.Mappings;

namespace RecipeSharingApi.BusinessLogic.Helpers;

public class AutoMapperConfigurations : Profile
{
    public AutoMapperConfigurations()
    {        
        CreateMap<RecipeCreateDTO, Recipe>().ForMember(src => src.Tags, dest => dest.Ignore());
        CreateMap<Recipe, RecipeDTO>().ForMember(src => src.TotalTime, dest => dest.MapFrom(x => x.PrepTime + x.CookTime));
        CreateMap<Recipe, RecipeUpdateDTO>().ReverseMap();

        CreateMap<Tag, TagCreateDTO>().ReverseMap();
        CreateMap<Tag, TagDTO>().ReverseMap();

        CreateMap<RecipeIngredient, RecipeIngredientCreateDTO>().ReverseMap();
        CreateMap<RecipeIngredient, RecipeIngredientDTO>().ReverseMap();
        CreateMap<RecipeIngredient, RecipeIngredientUpdateDTO>().ReverseMap();

        CreateMap<RecipeInstruction, RecipeInstructionCreateDTO>().ReverseMap();
        CreateMap<RecipeInstruction, RecipeInstructionDTO>().ReverseMap();
        CreateMap<RecipeInstruction, RecipeInstructionUpdateDTO>().ReverseMap();
        CreateMap<ReviewCreateDTO, Review>().ReverseMap();
        CreateMap<Review, ReviewUpdateDTO>().ReverseMap();
        CreateMap<Review, ReviewDTO>().ReverseMap();
        CreateMap<Cuisine, CuisineCreateDTO>().ReverseMap();
        CreateMap<Cuisine, CuisineDTO>().ReverseMap();
        CreateMap<CookBookCreateDTO,CookBook>().ForMember(src=>src.Recipes, dest =>dest.Ignore());
        CreateMap<User, UserDto>();
        CreateMap<CookBook, CookBookDTO>().ReverseMap();
        CreateMap<CollectionCreateDTO, Collection>().ForMember(src => src.Recipes, dest => dest.Ignore());
        CreateMap<CollectionDTO, Collection>().ReverseMap();

        CreateMap<User, UserRegisterDto>().ReverseMap();
        CreateMap<ShoppingListItem,ShoppingListItemDTO>().ReverseMap();
        CreateMap<User, UserLoginDTO>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => String.Format("{0} {1}", src.FirstName, src.LastName))).ReverseMap();

    }
}

