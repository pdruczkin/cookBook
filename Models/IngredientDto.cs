using System.ComponentModel.DataAnnotations;

namespace cookBook.Models
{
    public class IngredientDto
    { 
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Unit { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}