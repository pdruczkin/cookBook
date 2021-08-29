using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookBook.Entities.Api
{
    public class Step
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int RecipeId { get; set; }
        public virtual Recipe Recipe {  get; set; }

    }
}
