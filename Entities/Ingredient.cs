using System.Collections.Generic;

namespace cookBook.Entities
{
    public class Ingredient
    {
        public int IngredientId{ get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Amount { get; set; }
    }
}