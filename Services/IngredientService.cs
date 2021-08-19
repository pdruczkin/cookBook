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


            var ingredientEntity = _mapper.Map<Ingredient>(dto);
            ingredientEntity.RecipeIngredientId = recipeId;
            

           recipe.RecipeIngredients.Add(new RecipeIngredient()
           {
               Ingredient = ingredientEntity, IngredientId = ingredientEntity.IngredientId, Recipe = recipe, RecipeId = recipeId
           });

            _dbContext.Ingredients.Add(ingredientEntity);
            _dbContext.SaveChanges();

            return ingredientEntity.IngredientId;
        }
    }
}