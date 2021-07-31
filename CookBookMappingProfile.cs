using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using cookBook.Entities;
using cookBook.Entities.DifficultyProperties;
using cookBook.Entities.IngredientProperties;
using cookBook.Models;

namespace cookBook
{
    public class CookBookMappingProfile : Profile
    {
        public CookBookMappingProfile()
        {
           
            
            
            CreateMap<RecipeIngredient, IngredientsDto>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.Ingredient.Name))
                .ForMember(m => m.Amount, c => c.MapFrom(s => s.MeasurementQuantity.Amount))
                .ForMember(m => m.Description, c => c.MapFrom(s => s.MeasurementUnit.Description));


            CreateMap<Recipe, RecipeDto>()
                .ForMember(m => m.Ingredients, c => c.MapFrom(s => s.Ingredients))
                .ForMember(m => m.Steps, c => c.MapFrom(s => s.Steps.Select(y => y.Description)))
                .ForMember(m => m.Difficulty, c => c.MapFrom(s => s.Difficulty.Difficulty.Name));

            CreateMap<string, Step>()
                .ForMember(m => m, s => s.MapFrom(r => new Step() {Description = r}));

            CreateMap<string, Difficulty>()
                .ForMember(m => m, s => s.MapFrom(r => new Difficulty() {Name = r}));

            CreateMap<CreateRecipeDto, Recipe>();

        }
    }
}