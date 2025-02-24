using FluentValidation;
using GPS.API.Dtos.RegisterDtos;

namespace GPS.API.Validators.RegisterValidators
{
    public class RegisterManagerDtoValidator : AbstractValidator<RegisterManagerDto>
    {
        public RegisterManagerDtoValidator()
        {
            Include(new RegisterDtoValidator());

            RuleFor(x => x.HireDate)
                .GreaterThanOrEqualTo(new DateOnly(2000, 1, 1))
                .WithMessage("Hire date must be on or after January 1, 2000.");

            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("Department is required.")
                .Matches(@"^[\p{L}]+$").WithMessage("Department can only contain letters.");

            RuleFor(x => x.ManagerLevel)
                .NotEmpty().WithMessage("Manager level is required.")
                .Matches(@"^[\p{L}]+$").WithMessage("Manager level can only contain letters.");
        }
    }
}
