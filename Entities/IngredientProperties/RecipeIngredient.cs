using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace cookBook.Entities.IngredientProperties
{
    
    public class RecipeIngredient
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }


        public int MeasurementQuantityId { get; set; }
        public int MeasurementUnitId { get; set; }
        public int IngredientId { get; set; }

        public virtual MeasurementQuantity MeasurementQuantity { get; set; }
        public virtual MeasurementUnit MeasurementUnit { get; set; } 
        public virtual Ingredient Ingredient { get; set; }

    }
}
