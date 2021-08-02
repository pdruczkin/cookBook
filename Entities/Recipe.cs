using cookBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cookBook.Entities.DifficultyProperties;
using cookBook.Entities.IngredientProperties;

namespace cookBook.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PrepareTime { get; set; }
        public int SummaryTime { get; set; }

        
        public virtual ICollection<RecipeIngredient> Ingredients{ get; set; }
        public virtual List<Step> Steps { get; set; }
        public virtual Difficulty Difficulty { get; set; }
        






    
    }
}
