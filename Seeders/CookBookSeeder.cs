using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cookBook.Entities;

namespace cookBook.Seeders
{
    public class CookBookSeeder
    {
        private readonly CookBookDbContext _dbContext;

        public CookBookSeeder(CookBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Difficulties.Any())
                {
                    _dbContext.AddRange(DifficultiesSeeder.GetDifficulties());
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Recipes.Any())
                {
                    _dbContext.AddRange(RecipeSeeder.GetRecipes());
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Roles.Any())
                {
                    _dbContext.AddRange(RolesSeeder.GetRoles());
                    _dbContext.SaveChanges();
                }

            }
        }
    }
}