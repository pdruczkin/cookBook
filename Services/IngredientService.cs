using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using cookBook.Entities;
using cookBook.Exceptions;
using cookBook.Models;
using Microsoft.EntityFrameworkCore;

namespace cookBook.Services
{
    public interface IIngredientService
    {
        int Create(int restaurantId, IngredientDto dto);
        ShowIngredientDto GetById(int recipeId, int ingredientId);
        List<ShowIngredientDto> GetAll(int recipeId);
    }


    
    
    public class IngredientService : IIngredientService
    {
        private readonly CookBookDbContext _dbContext;
        private readonly IMapper _mapper;

        public IngredientService(CookBookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(int recipeId, IngredientDto dto)
        {
            var recipe = _dbContext
                .Recipes
                .Include(r => r.Steps)
                .Include(r => r.Difficulty)
                .Include(r => r.RecipeIngredients).ThenInclude(i => i.Ingredient)
                .FirstOrDefault(r => r.RecipeId == recipeId);
            if (recipe is null) throw new NotFoundException("Recipe not found");

            var foundIngredient = _dbContext
                .Ingredients
                .Include(r => r.RecipeIngredient)
                .FirstOrDefault(r => (r.Name == dto.Name && r.Amount == dto.Amount && r.Unit == dto.Unit));

            if (foundIngredient != null)
            {
                if (foundIngredient.RecipeIngredient.Any(recipeIngredient => recipeIngredient.IngredientId == foundIngredient.IngredientId && recipeIngredient.RecipeId == recipeId))
                {
                    throw new Exception("Bad data");//todo
                }

                return AddNewLink(recipeId, dto, recipe, foundIngredient);
            }
            return CreateNew(recipeId, dto, recipe);
        }

        private int CreateNew(int recipeId, IngredientDto dto, Recipe recipe)
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

            return ingredientEntity.IngredientId;
        }
        private int AddNewLink(int recipeId, IngredientDto dto, Recipe recipe, Ingredient foundIngredient)
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

            return foundIngredient.IngredientId;
        }

        
        
        
        public ShowIngredientDto GetById(int recipeId, int ingredientId)
        {
            var recipe = _dbContext
                .Recipes
                .Include(r => r.RecipeIngredients).ThenInclude(i => i.Ingredient)
                .FirstOrDefault(r => r.RecipeId == recipeId);


            if (recipe is null)
            {
                throw new NotFoundException("Recipe not found");
            }

            var ingredient = _dbContext.Ingredients.FirstOrDefault(i => i.IngredientId == ingredientId);

            if (ingredient is null)
            {
                throw new NotFoundException("Ingredient not found");
            }

            if (recipe.RecipeIngredients.Any(ing => ing.IngredientId == ingredientId) == false)
            {
                throw new NotFoundException("Ingredient not found in recipe");
            }

            var ingredientDto = _mapper.Map<ShowIngredientDto>(ingredient);


            return ingredientDto;
        }

        public List<ShowIngredientDto> GetAll(int recipeId)
        {
            var recipe = _dbContext
                .Recipes
                .Include(r => r.RecipeIngredients).ThenInclude(i => i.Ingredient)
                .FirstOrDefault(r => r.RecipeId == recipeId);

            if (recipe is null)
            {
                throw new NotFoundException("Recipe not found");
            }

            var listOfIngredient = recipe.RecipeIngredients.Select(recipeIngredient => recipeIngredient.Ingredient).ToList();

            var listOfIngredientDtos = _mapper.Map<List<ShowIngredientDto>>(listOfIngredient);

            return listOfIngredientDtos;
        }
    }


}