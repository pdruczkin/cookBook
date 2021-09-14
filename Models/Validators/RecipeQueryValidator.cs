using System.Linq;
using cookBook.Entities.Api;
using FluentValidation;

namespace cookBook.Models.Validators
{
    public class RecipeQueryValidator : AbstractValidator<RecipeQuery>
    {
        private readonly int[] _allowedPageSizes = new[] { 5, 10, 15 };

        private readonly string[] _allowedSortByColumnNames =
            new string[] { nameof(Recipe.Name), nameof(Recipe.Description) };
        public RecipeQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!_allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", _allowedPageSizes)}]");
                }
            });

            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || _allowedSortByColumnNames.Contains(value))
                .WithMessage($"SortBy is optional or must be in [ {string.Join(",", _allowedSortByColumnNames)}]");
        }
    }
}