using FluentValidation;
using GPS.API.Data.Models;

namespace GPS.API.Validators.DiscountValidators
{
    public class DiscountValidator : AbstractValidator<Discount>
    {
        public DiscountValidator()
        {
            RuleFor(x => x.DiscountName)
                .NotEmpty().WithMessage("Discount name is required.")
                .Matches(@"^[\p{L}\p{N}]+(?:\s+[\p{L}\p{N}]+)*$").WithMessage("Discount name must contain only letters and numbers.");

            RuleFor(x => x.DiscountValue)
                .NotEmpty().WithMessage("Discount value is required.")
                .InclusiveBetween(0.01f, 1f).WithMessage("Discount value must be between 0.01 and 1.");
        }
    }
}
