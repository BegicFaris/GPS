using FluentValidation;
using GPS.API.Dtos.StationDtos;

namespace GPS.API.Validators.StationValidators
{
    public class StationUpdateDtoValidator : AbstractValidator<StationUpdateDto>
    {
        public StationUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                      .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.ZoneId)
                .GreaterThan(0).When(x => x.ZoneId.HasValue).WithMessage("ZoneId must be greater than 0.");

            RuleFor(x => x.Name)
                .Matches(@"^[\p{L}\p{N}]+(?:\s+[\p{L}\p{N}]+)*$").When(x => !string.IsNullOrEmpty(x.Name)).WithMessage("Name must only contain letters and numbers.");

            RuleFor(x => x.Location)
                .Matches(@"^[\p{L}\p{N}]+(?:\s+[\p{L}\p{N}]+)*$").When(x => !string.IsNullOrEmpty(x.Location)).WithMessage("Location must only contain letters and numbers.");

            RuleFor(x => x.GPSCode)
                .Matches(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$")
                .When(x => !string.IsNullOrEmpty(x.GPSCode)).WithMessage("GPS Code must be in the correct format (e.g., 1.12, 9.78).");
        }
    }
}
