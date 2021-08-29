using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using cookBook.Entities;
using cookBook.Entities.Api;
using cookBook.Exceptions;
using cookBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace cookBook.Services
{
    public interface ICookBookService
    {
        IEnumerable<RecipeDto> GetAll();
        RecipeDto Get(int id);

        int CreateRecipe(CreateRecipeDto dto);

        void Delete(int id);

        void Update(int id, UpdateRecipeDto dto);

    }


    public class CookBookService : ICookBookService
    {
        private readonly CookBookDbContext _dbContext;

        private readonly IMapper _mapper;

        private readonly ILogger<CookBookService> _logger;

        public CookBookService(CookBookDbContext dbContext, IMapper mapper, ILogger<CookBookService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }


        public IEnumerable<RecipeDto> GetAll()
        {
           

            var recipies = _dbContext
                .Recipes
                .Include(r => r.Steps)
                .Include(r => r.Difficulty)
                .Include(r => r.RecipeIngredients).ThenInclude(i => i.Ingredient)
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
                .Include(r => r.RecipeIngredients).ThenInclude(i => i.Ingredient)
                .FirstOrDefault(r => r.RecipeId == id);

            if (recipe is null) throw new NotFoundException("Recipe not found");

            var recipeDto = _mapper.Map<RecipeDto>(recipe);

            return recipeDto;
        }

        public int CreateRecipe(CreateRecipeDto dto)
        {
            _logger.LogWarning("new Recipe action invoke");


            var recipe = _mapper.Map<Recipe>(dto);
            recipe.RecipeIngredients.Clear();

            var difficulty = _dbContext.Difficulties.FirstOrDefault(r => r.Name == dto.Difficulty);

            if(difficulty != null)
            {
                recipe.Difficulty = difficulty;
            }

            foreach (var ingredient in dto.Ingredients)
            {
                var foundIngredient = 
                    _dbContext
                        .Ingredients
                        .FirstOrDefault(r => (r.Name == ingredient.Name && r.Amount == ingredient.Amount && r.Unit == ingredient.Unit));

                if (foundIngredient != null)
                {
                    AddNewLink(recipe.RecipeId, ingredient, recipe, foundIngredient);
                }
                else
                {
                    CreateNew(recipe.RecipeId, ingredient, recipe);
                }
            }

            return recipe.RecipeId;
        }


        private void CreateNew(int recipeId, IngredientDto dto, Recipe recipe)
        {
            var ingredientEntity = _mapper.Map<Ingredient>(dto);

            var newRecipeIngredient = new RecipeIngredient()
            {
                Ingredient = ingredientEntity,
                IngredientId = ingredientEntity.IngredientId,
                Recipe = recipe,
                RecipeId = recipeId
            };
            recipe.RecipeIngredients.Add(newRecipeIngredient);
            ingredientEntity.RecipeIngredient = new List<RecipeIngredient>() { newRecipeIngredient };


            _dbContext.Ingredients.Add(ingredientEntity);
            _dbContext.SaveChanges();

        }
        private void AddNewLink(int recipeId, IngredientDto dto, Recipe recipe, Ingredient foundIngredient)
        {


            var newRecipeIngredient = new RecipeIngredient()
            {
                Ingredient = foundIngredient,
                IngredientId = foundIngredient.IngredientId,
                Recipe = recipe,
                RecipeId = recipeId
            };

            recipe.RecipeIngredients.Add(newRecipeIngredient);

            if (foundIngredient.RecipeIngredient == null)
            {
                foundIngredient.RecipeIngredient = new List<RecipeIngredient>();
            }
            foundIngredient.RecipeIngredient.Add(newRecipeIngredient);

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            //var ingredient = _dbContext.Ingredients.FirstOrDefault(i => i.IngredientId == 24);
            var recipe = _dbContext
                .Recipes
                .Include(r => r.Steps)
                .Include(r => r.RecipeIngredients).ThenInclude(i => i.Ingredient)
                .FirstOrDefault(r => r.RecipeId == id);

            if(recipe is null) throw new NotFoundException("Recipe not found");
            

            _dbContext.Recipes.Remove(recipe);
            _dbContext.SaveChanges();
        }


        public void Update(int id, UpdateRecipeDto dto)
        {
            var recipe = _dbContext
                .Recipes
                .Include(r => r.RecipeIngredients)
                .FirstOrDefault(r => r.RecipeId == id);

            if (recipe is null) throw new NotFoundException("Recipe not found");
            
            

            recipe.Name = dto.Name;
            recipe.PrepareTime = dto.PrepareTime;
            recipe.SummaryTime = dto.SummaryTime;
            recipe.Description = dto.Description;


            var newDifficulty = _dbContext.Difficulties.FirstOrDefault(r => r.Name == dto.Difficulty);
            recipe.Difficulty = newDifficulty;
            //recipe.Steps = (List<Step>)_mapper.Map<IEnumerable<Step>>(dto.Steps); ---podencja

            _dbContext.SaveChanges();
        }
    }
}