using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using cookBook.Validation;

namespace cookBook.Models
{
    public class CreateRecipeDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int PrepareTime { get; set; }
        [Required]
        public int SummaryTime { get; set; }
        [Required]
        
        public IEnumerable<IngredientDto> Ingredients { get; set; }

        [Steps(250)]
        public IEnumerable<string> Steps { get; set; }

        public string Difficulty { get; set; }

        
    }
}