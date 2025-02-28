using FluentValidation;
using GPS.API.Dtos.TicketDtos;

namespace GPS.API.Validators.TicketValidators
{
    public class TicketCreateDtoValidator : AbstractValidator<TicketCreateDto>
    {
        public TicketCreateDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required")
                .GreaterThan(0).WithMessage("UserId must be greater than 0");

            RuleFor(x => x.TicketInfoId)
                .NotEmpty().WithMessage("TicketInfoId is required")
                .GreaterThan(0).WithMessage("TicketInfoId must be greater than 0");

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("CreatedDate is required")
                .LessThanOrEqualTo(x => x.ExpirationDate).WithMessage("CreatedDate must be earlier than or equal to ExpirationDate");

            RuleFor(x => x.ExpirationDate)
                .NotEmpty().WithMessage("ExpirationDate is required")
                .GreaterThan(DateTime.Now).WithMessage("ExpirationDate must be in the future");

            RuleFor(x => x.QrCode)
                .NotEmpty().WithMessage("QrCode is required")
                .NotNull().WithMessage("QrCode cannot be null");
        }
    }
}
