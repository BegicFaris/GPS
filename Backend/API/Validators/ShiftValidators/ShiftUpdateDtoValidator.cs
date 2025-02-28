using FluentValidation;
using GPS.API.Dtos.ShiftDtos;

namespace GPS.API.Validators.ShiftValidators
{
    public class ShiftUpdateDtoValidator : AbstractValidator<ShiftUpdateDto>
    {
        public ShiftUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                 .GreaterThan(0)
                .WithMessage("Id must be greater than 0 if provided.")
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.BusId)
                .GreaterThan(0).When(x => x.BusId.HasValue)
                .WithMessage("Bus Id must be greater than 0 if provided.");

            RuleFor(x => x.DriverId)
                .GreaterThan(0).When(x => x.DriverId.HasValue)
                .WithMessage("Driver Id must be greater than 0 if provided.");

            RuleFor(x => x.ShiftDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .When(x => x.ShiftDate.HasValue)
                .WithMessage("Shift date cannot be in the past if provided.");

            RuleFor(x => x.ShiftStartingTime)
                .LessThan(x => x.ShiftEndingTime)
                .When(x => x.ShiftStartingTime.HasValue && x.ShiftEndingTime.HasValue)
                .WithMessage("Shift starting time must be before the ending time.");

        }
    }
}
