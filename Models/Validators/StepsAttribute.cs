﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using cookBook.Entities;
using cookBook.Models;

namespace cookBook.Models.Validators
{
    public class StepsAttribute : ValidationAttribute
    {
        public int MaxLength { get;}

        public string GetErrorMessage() =>
            $"Step content is null or its length is not between 0 and {MaxLength}.";

        public string NullErrorMessage = "List of Steps can't be empty";

        public StepsAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            
            List<string> steps = null;
           
            if (validationContext.ObjectInstance.GetType().Equals(typeof(CreateRecipeDto)))
            {
                steps = (List<string>)((CreateRecipeDto)validationContext.ObjectInstance).Steps;
            }
           
            



            if (steps == null || steps.Count == 0)
            {
                return new ValidationResult(NullErrorMessage);
            }

            if (steps.Any(step => (step == null || step.Length > MaxLength || step.Length == 0)))
            {
                return  new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}