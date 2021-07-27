using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookingBook.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int PrepareTime { get; set; }
        public int SummaryTime { get; set; }
        public DateTime Created { get; set; }

        public int DifficultyId { get; set; }
        public virtual Difficulty Difficulty{ get; set; }

        public virtual List<Ingredient> Ingredients { get; set; }

    
    }
}
