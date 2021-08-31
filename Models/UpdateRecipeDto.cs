using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cookBook.Models
{
    public class UpdateRecipeDto
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
        public string Difficulty { get; set; }

    }
}
