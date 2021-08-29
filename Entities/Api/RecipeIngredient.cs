﻿namespace cookBook.Entities.Api
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }


        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}