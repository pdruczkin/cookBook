using System.Collections.Generic;
using cookBook.Entities;
using cookBook.Entities.DifficultyProperties;
using cookBook.Entities.IngredientProperties;

namespace cookBook.Seeders
{
    public static class RecipeSeeder
    {
        public static List<Recipe> GetRecipes()
        {
            var list = new List<Recipe>()
            {
                new Recipe()
                {
                    Name = "Mojito",
                    Description = "refreshing, rum based drink",
                    PrepareTime = 5,
                    SummaryTime = 10,
                    Ingredients = new List<RecipeIngredient>()
                    {
                        new RecipeIngredient()
                        {
                            Ingredient = new Ingredient()
                            {
                                Name = "Bacardi"
                            },
                            MeasurementQuantity = new MeasurementQuantity()
                            {
                                Amount = 50
                            },
                            MeasurementUnit = new MeasurementUnit()
                            {
                                Description = "ml"
                            }
                        },
                        new RecipeIngredient()
                        {
                            Ingredient = new Ingredient()
                            {
                                Name = "mint"
                            },
                            MeasurementQuantity = new MeasurementQuantity()
                            {
                                Amount = 15
                            },
                            MeasurementUnit = new MeasurementUnit()
                            {
                                Description = "leaves"
                            }
                        }
                    },
                    Steps = new List<Step>()
                    {
                        new Step()
                        {
                            Description = "Pour all Bacardi into shaker"
                        },
                        new Step()
                        {
                            Description = "add half of mint leaves, rest use as decoration"
                        }
                    },
                    Difficulty = new RecipeDifficulty()
                    {
                        Difficulty = new Difficulty()
                        {
                            Name = "easy"
                        }
                    }
                    
                }
            };

            
           
            return list;
        }


    }
}