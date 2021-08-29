using System.Collections.Generic;

namespace cookBook.Entities.Api
{
    public class Ingredient
    {
        public int IngredientId{ get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Amount { get; set; }

        //public int RecipeIngredientId { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredient { get; set; }
    }
}