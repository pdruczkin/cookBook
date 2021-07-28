using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookBook.Entities.IngredientProperties
{
    public class MeasurementUnit
    {
        public int Id { get; set; }
        public string Description { get; set; }

        //public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
