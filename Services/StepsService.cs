using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using cookBook.Authorization;
using cookBook.Entities;
using cookBook.Entities.Api;
using cookBook.Exceptions;
using cookBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace cookBook.Services
{
    public interface IStepsService
    {
        List<string> GetAllSteps(int recipeId);

        void Update(int recipeId, List<string> newSteps);
        void Delete(int recipeId);
    }

    public class StepsService : IStepsService
    {
        private readonly CookBookDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public StepsService(CookBookDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public List<string> GetAllSteps(int recipeId)
        {
            var recipe = _dbContext
                .Recipes
                .Include(r => r.Steps)
                .FirstOrDefault(r => r.RecipeId == recipeId);

            if (recipe is null)
            {
                throw new NotFoundException("Recipe not found");
            }

            var steps = recipe.Steps;

            var stepsDto = _mapper.Map<List<string>>(steps);

            return stepsDto;
        }

        public void Update(int recipeId, List<string> newSteps)
        {
            var recipe = _dbContext
                .Recipes
                .Include(r => r.Steps)
                .FirstOrDefault(r => r.RecipeId == recipeId);

            if (recipe is null)
            {
                throw new NotFoundException("Recipe not found");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, recipe,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            var stepsToAdd = _mapper.Map<List<Step>>(newSteps);

            stepsToAdd.ForEach(s => s.RecipeId = recipeId);

            recipe.Steps = stepsToAdd;

            _dbContext.Recipes.Update(recipe);
            _dbContext.SaveChanges();
        }

        public void Delete(int recipeId)
        {
            var recipe = _dbContext
                .Recipes
                .Include(r => r.Steps)
                .FirstOrDefault(r => r.RecipeId == recipeId);

            if (recipe is null)
            {
                throw new NotFoundException("Recipe not found");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, recipe,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            recipe.Steps.Clear();
            
            _dbContext.Recipes.Update(recipe);
            _dbContext.SaveChanges();
        }
    }
}