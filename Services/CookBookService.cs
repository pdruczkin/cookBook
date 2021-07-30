﻿using System.Collections.Generic;
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
                .Include(r => r.Difficulty).ThenInclude(s => s.Difficulty)
                .Include(r => r.Ingredients).ThenInclude(s => s.MeasurementQuantity)
                .Include(r => r.Ingredients).ThenInclude(s => s.MeasurementUnit)
                .Include(r => r.Ingredients).ThenInclude(s => s.Ingredient)
                .ToList();

            var recipiesDto = _mapper.Map<List<RecipeDto>>(recipies);

            return recipiesDto;
        }






    }
}