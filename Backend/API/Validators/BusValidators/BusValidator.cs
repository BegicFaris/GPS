using FluentValidation;
using GPS.API.Data.Models;

namespace GPS.API.Validators.BusValidators
{

    public class BusValidator : AbstractValidator<Bus>
    {
        public BusValidator()
        {
            RuleFor(x => x.RegistrationNumber)
                .NotEmpty().WithMessage("Registration number is required.")
                .Matches(@"^[\p{L}\p{N}]+(?:[ -]*[\p{L}\p{N}]+)*$").WithMessage("Registration number can only contain letters, numbers, and dashes.");

            RuleFor(x => x.Manufacturer)
                .NotEmpty().WithMessage("Manufacturer is required.");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required.");

            RuleFor(x => x.Capacity)
                .NotEmpty().WithMessage("Capacity is required.")
                .Must(BeAValidPositiveNumber).WithMessage("Capacity must be a positive number.");

            RuleFor(x => x.ManufactureYear)
                .NotEmpty().WithMessage("Manufacture year is required.")
                .Must(BeAValidYear).WithMessage("Manufacture year must be a valid year(between 1900 and now).");
        }

        // Helper method to check if string is a valid positive number (for Capacity)
        private bool BeAValidPositiveNumber(string capacity)
        {
            if (string.IsNullOrEmpty(capacity)) return false;

            if (int.TryParse(capacity, out var result))
            {
                return result > 0; // Capacity must be a positive number
            }

            return false; // Return false if it's not a valid number
        }

        // Helper method to check if string is a valid year (for ManufactureYear)
        private bool BeAValidYear(string year)
        {
            if (string.IsNullOrEmpty(year)) return false;

            if (int.TryParse(year, out var result))
            {
                return result >= 1900 && result <= DateTime.Now.Year; // Year must be between 1900 and the current year
            }

            return false; // Return false if it's not a valid number
        }
    }

}
