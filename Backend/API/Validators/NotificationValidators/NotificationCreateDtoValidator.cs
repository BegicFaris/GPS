using FluentValidation;
using GPS.API.Data.Models;

namespace GPS.API.Validators.NotificationValidators
{
    public class NotificationCreateDtoValidator : AbstractValidator<Notification>
    {
        public NotificationCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Matches(@"^[\p{L}\p{N}]+(?:\s+[\p{L}\p{N}]+)*$").WithMessage("Title can only contain letters, numbers, and spaces.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters long.");

            RuleFor(x => x.NotificationTypeId)
                .GreaterThan(0).WithMessage("NotificationTypeId must be greater than 0.");

            RuleFor(x => x.LineId)
                .GreaterThan(0).When(x => x.LineId.HasValue)
                .WithMessage("LineId must be greater than 0 if provided.");

            RuleFor(x => x.CreationDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreationDate cannot be in the future.");

            RuleFor(x => x.ManagerId)
                .GreaterThan(0).WithMessage("ManagerId must be greater than 0.");
        }
    }
}
