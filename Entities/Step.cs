using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookBook.Entities
{
    public class Step
    {
        public int Id { get; set; }
        public int Description { get; set; }

        public int RecipeId { get; set; }
        public virtual Recipe Recipe {  get; set; }

    }
}
