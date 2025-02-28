using FluentValidation;
using GPS.API.Dtos.RouteDtos;
using GPS.API.Interfaces;

namespace GPS.API.Validators.RouteValidators
{
    public class RouteCreateDtoValidator : AbstractValidator<RouteCreateDto>
    {
        IRouteService _routeService;
        public RouteCreateDtoValidator(IRouteService routeService)
        {
            _routeService = routeService;
            RuleFor(x => x.LineId)
                .NotEmpty().WithMessage("LineId is required.")
                .GreaterThan(0).WithMessage("LineId must be greater than 0.");

            RuleFor(x => x.StationId)
                .NotEmpty().WithMessage("StationId is required.")
                .GreaterThan(0).WithMessage("StationId must be greater than 0.");

            RuleFor(x => x.DistanceFromTheNextStation)
                .NotEmpty().WithMessage("DistanceFromTheNextStation is required.")
                .GreaterThan(TimeOnly.MinValue).WithMessage("DistanceFromTheNextStation must be a valid time value.");

            RuleFor(x => x.Order)
                .NotEmpty().WithMessage("Order is required.");
        }
    }
}
