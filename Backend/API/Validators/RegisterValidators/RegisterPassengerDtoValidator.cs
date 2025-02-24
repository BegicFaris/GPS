using FluentValidation;
using GPS.API.Dtos.RegisterDtos;

namespace GPS.API.Validators.RegisterValidators
{
    public class RegisterPassengerDtoValidator : AbstractValidator<RegisterPassengerDto>
    {
        public RegisterPassengerDtoValidator()
        {
            Include(new RegisterDtoValidator()); // Nasleđuje pravila iz RegisterDtoValidator

            RuleFor(x => x.CaptchaResponse)
                .NotEmpty().WithMessage("Captcha response is required.");

            RuleFor(x => x.DiscountId)
                .GreaterThanOrEqualTo(1).When(x => x.DiscountId.HasValue)
                .WithMessage("Discount ID must be 1 or greater.");
        }
    }
}
