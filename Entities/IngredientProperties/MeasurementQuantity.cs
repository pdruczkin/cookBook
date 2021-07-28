using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookBook.Entities.IngredientProperties
{
    public class MeasurementQuantity
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        //public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }

    }
}
