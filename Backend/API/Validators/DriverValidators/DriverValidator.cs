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
                .NotEmpty().WithMessage("First name is required.")
                .Matches(@"^[\p{L}]+(?:\s+[\p{L}]+)*$").WithMessage("First name can only contain letters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Matches(@"^[\p{L}]+(?:\s+[\p{L}]+)*$").WithMessage("Last name can only contain letters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");

            RuleFor(x => x.HireDate)
                .NotEmpty().WithMessage("Hire date is required.")
                .GreaterThanOrEqualTo(new DateOnly(2000, 1, 1)).WithMessage("Hire date must be on or after 01.01.2000.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Birth date cannot be in the future.");

            RuleFor(x => x.License)
                .NotEmpty().WithMessage("License is required.")
                .Matches(@"^[\p{L}\p{N}]+(?:\s+[\p{L}\p{N}]+)*$").WithMessage("License can only contain letters and numbers.");

            RuleFor(x => x.DriversLicenseNumber)
                .NotEmpty().WithMessage("Drivers license number is required.")
                .Matches(@"^[\p{L}\p{N}]+(?:[ -]*[\p{L}\p{N}]+)*$").WithMessage("Drivers license can only contain letters,numbers and dashes.");

            RuleFor(x => x.WorkingHoursInAWeek)
                .InclusiveBetween(0, 80).WithMessage("Working hours must be between 0 and 80 hours per week.");

            RuleFor(x => x.BirthDate)
                .GreaterThanOrEqualTo(new DateOnly(1900, 1, 1)).WithMessage("Birth date must be on or after 01.01.1900.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Birth date cannot be in the future.");

            RuleFor(x => x.RegistrationDate)
                .Must(date => date == null || (date.Value.Year >= 2000 && date.Value.Date <= DateTime.Today))
                .WithMessage("Registration date must be from the year 2000 or later and cannot be in the future.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address cannot be empty.");
        }
    }
}
