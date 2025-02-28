using FluentValidation;
using GPS.API.Data.Models;

namespace GPS.API.Validators.TicketInfoValidators
{
    public class TicketInfoValidator : AbstractValidator<TicketInfo>
    {
        public TicketInfoValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.ZoneId)
                .GreaterThan(0).WithMessage("ZoneId must be greater than 0.");

            RuleFor(x => x.TicketTypeId)
                .GreaterThan(0).WithMessage("TicketTypeId must be greater than 0.");

            RuleFor(x => x.Zone)
                .NotNull().WithMessage("Zone is required.");

            RuleFor(x => x.TicketType)
                .NotNull().WithMessage("TicketType is required.");
        }
    }
}
