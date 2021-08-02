using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookBook.Entities.DifficultyProperties
{
    public class Difficulty
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Recipe> Recipes { get; set; }
        
    }
}
