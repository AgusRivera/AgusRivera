using AMVTravels.Models.Entities;
using FluentValidation;

namespace AMVTravels.Validators
{
    public class TourValidator : AbstractValidator<Tour>
    {
        public TourValidator()
        {
            RuleFor(Tour => Tour)
                .NotEmpty()
                .NotNull()
                .WithMessage("Los Datos del tour no pueden ser nulos");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El campo NOMBRE no puede quedar vació, ingrese un valor")
                .NotNull().WithMessage("El campo NOMBRE no puede ser Nulo, ingrese un valor")
                .MaximumLength(100).WithMessage("El campo NOMBRE tiene un maximo de 100 caracteres");

            RuleFor(x => x.Destination)
                .NotEmpty().WithMessage("El campo DETINO no puede quedar vació, ingrese un valor")
                .NotNull().WithMessage("El campo DESTINO no puede ser Nulo, ingrese un valor")
                .MaximumLength(100).WithMessage("El campo DESTINO tiene un maximo de 100 caracteres");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("El campo FECHA INICIO no puede quedar vació, ingrese un valor")
                .NotNull().WithMessage("El campo FECHA INICIO no puede ser Nulo, ingrese un valor");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("El campo PRECIO no puede quedar vació, ingrese un valor")
                .NotNull().WithMessage("El campo PRECIO no puede ser Nulo, ingrese un valor");


        }
    }
}
