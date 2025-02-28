using FluentValidation;
using GPS.API.Dtos.NotificationDtos;

namespace GPS.API.Validators.NotificationValidators
{
    public class NotificationUpdateDtoValidator: AbstractValidator<NotificationUpdateDto>
    {
        public NotificationUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
            
            RuleFor(x => x.Title)
                .Matches(@"^[\p{L}\p{N}]+(?:\s+[\p{L}\p{N}]+)*$").When(x => !string.IsNullOrWhiteSpace(x.Title))
                .WithMessage("Title can only contain letters, numbers, and spaces.");

            RuleFor(x => x.Description)
                .MinimumLength(10).When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage("Description must be at least 10 characters long.");

            RuleFor(x => x.NotificationTypeId)
                .GreaterThan(0).When(x => x.NotificationTypeId.HasValue)
                .WithMessage("NotificationTypeId must be greater than 0 if provided.");

            RuleFor(x => x.LineId)
                .GreaterThan(0).When(x => x.LineId.HasValue)
                .WithMessage("LineId must be greater than 0 if provided.");

            RuleFor(x => x.CreationDate)
                .LessThanOrEqualTo(DateTime.UtcNow).When(x => x.CreationDate.HasValue)
                .WithMessage("CreationDate cannot be in the future.");

            RuleFor(x => x.ManagerId)
                .GreaterThan(0).When(x => x.ManagerId.HasValue)
                .WithMessage("ManagerId must be greater than 0 if provided.");
        }
    }
}
