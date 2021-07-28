using cookBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookBook
{
    public class CookBookSeeder
    {
        private readonly CookBookDbContext _dbContext;
        public CookBookSeeder(CookBookDbContext dbContext)
        {
            _dbContext = dbContext;

        }
    }
}
