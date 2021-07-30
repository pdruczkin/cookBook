﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace cookBook.Entities.DifficultyProperties
{
    
    public class RecipeDifficulty
    {
        public int Id { get; set; }
        public int DifficultyId { get; set; }
        public virtual Difficulty Difficulty { get; set; }

        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }


    }
}
