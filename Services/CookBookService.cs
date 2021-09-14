using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using AutoMapper;
using cookBook.Authorization;
using cookBook.Entities;
using cookBook.Entities.Api;
using cookBook.Exceptions;
using cookBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace cookBook.Services
{
    public interface ICookBookService
    {
        PageResult<RecipeDto> GetAll(RecipeQuery query);
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
        
        private readonly IAuthorizationService _authorizationService;

        private readonly IUserContextService _userContextService;

        public CookBookService(CookBookDbContext dbContext, IMapper mapper, ILogger<CookBookService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }


        public PageResult<RecipeDto> GetAll(RecipeQuery query)
        {
           
            var baseQuery = _dbContext
                .Recipes
                .Include(r => r.Steps)
                .Include(r => r.Difficulty)
                .Include(r => r.RecipeIngredients).ThenInclude(i => i.Ingredient)
                .Where(r => query.SearchPhrase == null || (r.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                                                           || r.Description.ToLower()
                                                               .Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Recipe, object>>>
                {
                    { nameof(Recipe.Name), r => r.Name },
                    { nameof(Recipe.Description), r => r.Description }
                };

                var selectedColumn = columnsSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.Asc
                    ? baseQuery.OrderBy(r => r.Name)
                    : baseQuery.OrderByDescending(r => r.Description);
            }

            var recipies = baseQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var recipiesDto = _mapper.Map<List<RecipeDto>>(recipies);


            var totalAmount = baseQuery.Count();


            var pageResult = new PageResult<RecipeDto>(recipiesDto, totalAmount, query.PageSize, query.PageNumber);

            return pageResult;
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

            recipe.CreatedById = _userContextService.GetUserId; 

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
            
            var recipe = _dbContext
                .Recipes
                .Include(r => r.Steps)
                .Include(r => r.RecipeIngredients).ThenInclude(i => i.Ingredient)
                .FirstOrDefault(r => r.RecipeId == id);

            if(recipe is null) throw new NotFoundException("Recipe not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, recipe,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }



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

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, recipe,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }


            recipe.Name = dto.Name;
            recipe.PrepareTime = dto.PrepareTime;
            recipe.SummaryTime = dto.SummaryTime;
            recipe.Description = dto.Description;


            var newDifficulty = _dbContext.Difficulties.FirstOrDefault(r => r.Name == dto.Difficulty);
            recipe.Difficulty = newDifficulty;
            

            _dbContext.SaveChanges();
        }
    }
}