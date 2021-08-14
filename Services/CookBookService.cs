﻿using System;
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
                

            var recipeDto = _mapper.Map<RecipeDto>(recipe);

            return recipeDto;
        }

        public int CreateRecipe(CreateRecipeDto dto)
        {
            var recipeIngredient = _mapper.Map<RecipeIngredient>(dto.Ingredients.FirstOrDefault());

            var ingredient = _mapper.Map<Ingredient>(dto.Ingredients.FirstOrDefault());

            var recipeIngredients = _mapper.Map<ICollection<RecipeIngredient>>(dto.Ingredients);

            var recipe1 = _mapper.Map<Recipe>(dto.Ingredients);


            var recipe = _mapper.Map<Recipe>(dto);

            _dbContext.Recipes.Add(recipe);
            _dbContext.SaveChanges();


            return recipe.RecipeId;
        }

       

        

    }
}