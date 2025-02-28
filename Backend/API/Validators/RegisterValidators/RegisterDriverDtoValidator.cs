using FluentValidation;
using GPS.API.Dtos.RegisterDtos;
using GPS.API.Validators.RegisterValidators;

public class RegisterDriverDtoValidator : AbstractValidator<RegisterDriverDto>
{
    public RegisterDriverDtoValidator()
    {
        Include(new RegisterDtoValidator()); // Nasleđuje pravila iz RegisterDtoValidator

        RuleFor(x => x.License)
            .NotEmpty().WithMessage("License is required.")
            .Matches(@"^[\p{L}\p{N}]+(?:\s+[\p{L}\p{N}]+)*$").WithMessage("License can only contain letters and numbers.");
            
        RuleFor(x => x.DriversLicenseNumber)
            .NotEmpty().WithMessage("Driver's license number is required.")
            .Matches(@"^[\p{L}\p{N}]+(?:[ -]*[\p{L}\p{N}]+)*$").WithMessage("Driver's license number can only contain letters, numbers and dashes.");

        RuleFor(x => x.HireDate)
            .GreaterThanOrEqualTo(new DateOnly(2000, 1, 1))
            .WithMessage("Hire date must be on or after January 1, 2000.");

        RuleFor(x => x.WorkingHoursInAWeek)
            .GreaterThanOrEqualTo(0).When(x => x.WorkingHoursInAWeek.HasValue)
            .WithMessage("Working hours must be at least 0.")
            .LessThanOrEqualTo(80).When(x => x.WorkingHoursInAWeek.HasValue)
            .WithMessage("Working hours cannot exceed 80.");
    }
}