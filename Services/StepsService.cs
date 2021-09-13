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
    public interface IStepsService
    {
        List<string> GetAllSteps(int recipeId);
    }

    public class StepsService : IStepsService
    {
        private readonly CookBookDbContext _dbContext;
        private readonly IMapper _mapper;

        public StepsService(CookBookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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


    }
}