﻿using System.Collections.Generic;
using cookBook.Entities;

namespace cookBook.Models
{
    public class RecipeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PrepareTime { get; set; }
        public int SummaryTime { get; set; }

        public IEnumerable<IngredientsDto> Ingredients { get; set; }

        public IEnumerable<string> Steps { get; set; }

        public string Difficulty { get; set; }

    }
}