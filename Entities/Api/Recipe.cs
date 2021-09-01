using cookBook.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using cookBook.Entities.Users;


namespace cookBook.Entities.Api
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PrepareTime { get; set; }
        public int SummaryTime { get; set; }

        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        
        public virtual ICollection<RecipeIngredient> RecipeIngredients{ get; set; }


        public virtual List<Step> Steps { get; set; }
        public virtual Difficulty Difficulty { get; set; }
        






    
    }
}
