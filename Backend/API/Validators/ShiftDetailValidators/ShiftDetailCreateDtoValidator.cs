using FluentValidation;
using GPS.API.Dtos.ShiftDetailDtos;

namespace GPS.API.Validators.ShiftDetailValidators
{
    public class ShiftDetailCreateDtoValidator : AbstractValidator<ShiftDetailCreateDto>
    {
        public ShiftDetailCreateDtoValidator()
        {
            RuleFor(x => x.ShiftId)
                .GreaterThan(0).WithMessage("ShiftId must be greater than 0.");

            RuleFor(x => x.LineId)
                .GreaterThan(0).WithMessage("LineId must be greater than 0.");

            RuleFor(x => x.ShiftDetailStartingTime)
                .NotEmpty().WithMessage("Shift detail starting time is required.");


            RuleFor(x => x.ShiftDetailEndingTime)
                .NotEmpty().WithMessage("Shift detail ending time is required.");
 

            RuleFor(x => x)
                .Custom((dto, context) =>
                {
                    try
                    {
                        var startingTime = TimeOnly.Parse(dto.ShiftDetailStartingTime);
                        var endingTime = TimeOnly.Parse(dto.ShiftDetailEndingTime);
                        if (startingTime >= endingTime)
                        {
                            context.AddFailure("Shift detail starting time must be before ending time.");
                        }
                    }
                    catch (FormatException)
                    {
                        context.AddFailure("Invalid formats"); 
                    }
                });
        }
    }
}
