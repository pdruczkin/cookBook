using System.Collections.Generic;

namespace cookBook.Models
{
    public class CreateRecipeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PrepareTime { get; set; }
        public int SummaryTime { get; set; }

        public IEnumerable<IngredientDto> Ingredients { get; set; }

        public IEnumerable<string> Steps { get; set; }

        public string Difficulty { get; set; }

        
    }
}