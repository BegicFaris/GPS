using FluentValidation;
using GPS.API.Data.Models;
using GPS.API.Dtos.RegisterDtos;
using GPS.API.Validators.RegisterValidators;

namespace GPS.API.Validators.DriverValidators
{
    public class DriverValidator : AbstractValidator<Driver>
    {
        public DriverValidator()
        {
            RuleFor(x => x.FirstName)
                .Matches(@"^[\p{L}]+$").WithMessage("First name can only contain letters.");

            RuleFor(x => x.LastName)
                .Matches(@"^[\p{L}]+$").WithMessage("Last name can only contain letters.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email must be a valid email address.");

            RuleFor(x => x.HireDate)
                .GreaterThanOrEqualTo(new DateOnly(2000, 1, 1)).WithMessage("Hire date must be on or after 01.01.2000.");

            RuleFor(x => x.License)
                .Matches(@"^[A-Za-z0-9]+$").WithMessage("License can only contain letters and numbers.");

            RuleFor(x => x.DriversLicenseNumber)
                .Matches(@"^[A-Za-z0-9]+$").WithMessage("Driver's license number can only contain letters and numbers.");

            RuleFor(x => x.WorkingHoursInAWeek)
                .InclusiveBetween(0, 80).WithMessage("Working hours must be between 0 and 80 hours per week.");

            RuleFor(x => x.BirthDate)
                .GreaterThanOrEqualTo(new DateOnly(1900, 1, 1)).WithMessage("Birth date must be on or after 01.01.1900.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Birth date cannot be in the future.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address cannot be empty.");
        }
    }
}
