using FluentValidation;
using GPS.API.Data.Models;
using System.Globalization;

namespace GPS.API.Validators.LineValidators
{
    public class LineValidator : AbstractValidator<Line>
    {
        public LineValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Matches(@"^\d+:\s+[\p{L}]+(?:\s+[\p{L}]+)*\s*-\s*[\p{L}]+(?:\s+[\p{L}]+)*$")
                .WithMessage("Name must be in the format 'number: text - text'.");

            RuleFor(x => x.CompleteDistance)
                .NotEmpty().WithMessage("Complete distance is required.")
                .Must(BeAValidPositiveNumber).WithMessage("Complete distance must be a positive number.");
        }

        private bool BeAValidPositiveNumber(string capacity)
        {
            if (string.IsNullOrEmpty(capacity)) return false;

            if (int.TryParse(capacity, out var result))
            {
                return result > 0; 
            }

            return false; 
        }

    }


}
