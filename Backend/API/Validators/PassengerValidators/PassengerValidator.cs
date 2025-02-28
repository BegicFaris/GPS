using FluentValidation;
using GPS.API.Data.Models;

namespace GPS.API.Validators.PassengerValidators
{
    public class PassengerValidator : AbstractValidator<Passenger>
    {
        public PassengerValidator()
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

            RuleFor(x => x.BirthDate)
                .GreaterThanOrEqualTo(new DateOnly(1900, 1, 1)).WithMessage("Birth date must be on or after 01.01.1900.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Birth date cannot be in the future.");

            RuleFor(x => x.RegistrationDate)
                 .Must(date => date == null || (date.Value.Year >= 2000 && date.Value <= DateTime.Today))
                 .WithMessage("Registration date must be from the year 2000 or later and cannot be in the future.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address cannot be empty.");


        }

    }
}
