using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using cookBook.Entities;
using cookBook.Entities.Api;
using cookBook.Entities.Users;
using cookBook.Models;

namespace cookBook
{
    public class CookBookMappingProfile : Profile
    {
        public CookBookMappingProfile()
        {



            CreateMap<RecipeIngredient, IngredientDto>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.Ingredient.Name))
                .ForMember(m => m.Amount, c => c.MapFrom(s => s.Ingredient.Amount))
                .ForMember(m => m.Unit, c => c.MapFrom(s => s.Ingredient.Unit));
                
            CreateMap<Recipe, RecipeDto>()
                .ForMember(m => m.Ingredients, c => c.MapFrom(s => s.RecipeIngredients))
                .ForMember(m => m.Steps, c => c.MapFrom(s => s.Steps.Select(y => y.Description)))
                .ForMember(m => m.Difficulty, c => c.MapFrom(s => s.Difficulty.Name));

            CreateMap<String, Recipe>();
            CreateMap<String, Difficulty>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s));


            CreateMap<String, Step>()
                .ForMember(m => m.Description, c => c.MapFrom(s => s));
            CreateMap<ICollection<String>, Recipe>();



            CreateMap<IngredientDto, RecipeIngredient>()
                .ForMember(m => m.Ingredient, c => c.MapFrom(s => s));

            CreateMap<IngredientDto, Ingredient>();



            //CreateMap<ICollection<IngredientDto>, ICollection<RecipeIngredient>>();



            CreateMap<ICollection<IngredientDto>, Recipe>()
                .ForMember(m => m.RecipeIngredients, c => c.MapFrom(s => s));


            CreateMap<CreateRecipeDto, Recipe>()
                .ForMember(m => m.RecipeIngredients, c => c.MapFrom(s => s.Ingredients));


            CreateMap<Ingredient, ShowIngredientDto>();

            CreateMap<RegisterUserDto, User>();

        }
    }
}