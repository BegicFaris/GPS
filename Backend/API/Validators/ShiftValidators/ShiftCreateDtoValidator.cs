using FluentValidation;
using GPS.API.Dtos.ShiftDtos;

namespace GPS.API.Validators.ShiftValidators
{
    public class ShiftCreateDtoValidator : AbstractValidator<ShiftCreateDto>
    {
        public ShiftCreateDtoValidator()
        {
            RuleFor(x => x.BusId)
                .NotEmpty().WithMessage("Bus ID is required.")
                .GreaterThan(0).WithMessage("Bus ID must be greater than 0.");

            RuleFor(x => x.DriverId)
                .NotEmpty().WithMessage("Driver ID is required.")
                .GreaterThan(0).WithMessage("Driver ID must be greater than 0.");

            RuleFor(x => x.ShiftDate)
                .NotEmpty().WithMessage("Shift date is required.")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Shift date cannot be in the past.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today.AddYears(1)))
                .WithMessage("Shift date cannot be more than one year in the future.");

            RuleFor(x => x.ShiftStartingTime)
                .NotEmpty().WithMessage("Shift starting time is required.")
                .LessThan(x => x.ShiftEndingTime)
                .WithMessage("Shift starting time must be before the ending time.");

            RuleFor(x => x.ShiftEndingTime)
                .NotEmpty().WithMessage("Shift ending time is required.");
        }
    }
}
