using FluentValidation;
using GPS.API.Dtos.FavoriteLineDtos;

namespace GPS.API.Validators.FavoriteLineValidators
{
    public class FavoriteLineCreateDtoValidator : AbstractValidator<FavoriteLineCreateDto>
    {
        public FavoriteLineCreateDtoValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");

            RuleFor(x => x.LineId)
                .GreaterThan(0).WithMessage("LineId must be greater than 0.");
        }
    }
}
