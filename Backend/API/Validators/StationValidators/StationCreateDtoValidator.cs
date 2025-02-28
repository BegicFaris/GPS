using FluentValidation;
using GPS.API.Dtos.StationDtos;

namespace GPS.API.Validators.StationValidators
{
    public class StationCreateDtoValidator : AbstractValidator<StationCreateDto>
    {
        public StationCreateDtoValidator()
        {
            RuleFor(x => x.ZoneId)
                .GreaterThan(0).WithMessage("ZoneId must be greater than 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Matches(@"^[\p{L}\p{N}]+(?:\s+[\p{L}\p{N}]+)*$").WithMessage("Name must only contain letters and numbers.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.")
                .Matches(@"^[\p{L}\p{N}]+(?:\s+[\p{L}\p{N}]+)*$").WithMessage("Location must only contain letters and numbers.");

            RuleFor(x => x.GPSCode)
                .NotEmpty().WithMessage("GPS Code is required.")
                .Matches(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$")
                .WithMessage("GPS Code must be in the correct format (e.g., 1.12, 9.78).");
        }
    }
}
