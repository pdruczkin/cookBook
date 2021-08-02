using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using cookBook.Entities;
using cookBook.Models;
using Microsoft.EntityFrameworkCore;

namespace cookBook.Services
{
    public interface ICookBookService
    {
        IEnumerable<RecipeDto> GetAll();
        RecipeDto Get(int id);

        int CreateRecipe(CreateRecipeDto dto);

    }


    public class CookBookService : ICookBookService
    {
        private readonly CookBookDbContext _dbContext;

        private readonly IMapper _mapper;


        public CookBookService(CookBookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public IEnumerable<RecipeDto> GetAll()
        {
            var recipies = _dbContext
                .Recipes
                .Include(r => r.Steps)
                .Include(r => r.Difficulty)
                .Include(r => r.Ingredients).ThenInclude(s => s.MeasurementQuantity)
                .Include(r => r.Ingredients).ThenInclude(s => s.MeasurementUnit)
                .Include(r => r.Ingredients).ThenInclude(s => s.Ingredient)
                .ToList();

            var recipiesDto = _mapper.Map<List<RecipeDto>>(recipies);

            return recipiesDto;
        }

        public RecipeDto Get(int id)
        {
            var recipe = _dbContext
                .Recipes
                .Include(r => r.Steps)
                .Include(r => r.Difficulty)
                .Include(r => r.Ingredients).ThenInclude(s => s.MeasurementQuantity)
                .Include(r => r.Ingredients).ThenInclude(s => s.MeasurementUnit)
                .Include(r => r.Ingredients).ThenInclude(s => s.Ingredient)
                .FirstOrDefault(r => r.Id == id);

            var recipeDto = _mapper.Map<RecipeDto>(recipe);

            return recipeDto;
        }

        public int CreateRecipe(CreateRecipeDto dto)
        {
            var recipe = MapFromDto(dto);

            return recipe.Id;

        }

        private Recipe MapFromDto(CreateRecipeDto dto)
        {
            Recipe recipe = new Recipe()
            {
                Name = dto.Name,
                Description = dto.Description,
                PrepareTime = dto.PrepareTime,
                SummaryTime = dto.SummaryTime,
                Steps = GetStepsFromStrings(dto.Steps),
                Difficulty = _dbContext.Difficulties.FirstOrDefault(r => r.Name == dto.Difficulty)
            };

            return recipe;
        }

        private List<Step> GetStepsFromStrings(IEnumerable<string> list)
        {
            List<Step> steps = new List<Step>();


            foreach (var textStep in list)
            {
                steps.Add(new Step() { Description = textStep });
            }

            return steps;
        }

}
}