using FluentValidation;
using GPS.API.Dtos.TicketDtos;

namespace GPS.API.Validators.TicketValidators
{
    public class TicketUpdateDtoValidator : AbstractValidator<TicketUpdateDto>
    {
        public TicketUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");

            RuleFor(x => x.UserId)
                .GreaterThan(0).When(x => x.UserId.HasValue).WithMessage("UserId must be greater than 0");

            RuleFor(x => x.TicketInfoId)
                .GreaterThan(0).When(x => x.TicketInfoId.HasValue).WithMessage("TicketInfoId must be greater than 0");

            RuleFor(x => x.CreatedDate)
                .LessThanOrEqualTo(x => x.ExpirationDate).When(x => x.CreatedDate.HasValue && x.ExpirationDate.HasValue)
                .WithMessage("CreatedDate must be earlier than or equal to ExpirationDate");

            RuleFor(x => x.ExpirationDate)
                .GreaterThan(DateTime.Now).When(x => x.ExpirationDate.HasValue)
                .WithMessage("ExpirationDate must be in the future");

            RuleFor(x => x.QrCode)
                .NotNull().When(x => x.QrCode != null).WithMessage("QrCode cannot be null");
        }
    }
}
