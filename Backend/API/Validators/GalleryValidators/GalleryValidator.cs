using FluentValidation;
using GPS.API.Data.Models;

namespace GPS.API.Validators.GalleryValidators
{
    public class GalleryValidator : AbstractValidator<Gallery>
    {
        public GalleryValidator()
        {
            RuleFor(x => x.UploadDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Upload date cannot be in the future.");

            RuleFor(x => x.Position)
                .GreaterThanOrEqualTo(0).WithMessage("Position must be a non-negative number.");
        }
    }
}
