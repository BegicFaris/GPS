using FluentValidation;
using GPS.API.Dtos.RegisterDtos;
using System.Text.RegularExpressions;

namespace GPS.API.Validators.RegisterValidators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Matches(@"^[\p{L}]+$").WithMessage("First name can only contain letters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Matches(@"^[\p{L}]+$").WithMessage("Last name can only contain letters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.BirthDate)
                .Must(date => date == null || (date.Value.Year >= 1900 && date.Value <= DateOnly.FromDateTime(DateTime.UtcNow)))
                .WithMessage("Birth date must be between 1900 and today.");

            RuleFor(x => x.RegistrationDate)
                .Must(date => date == null || date.Value.Year >= 2000)
                .WithMessage("Registration date must be from the year 2000 or later.");
        }
    }
}
