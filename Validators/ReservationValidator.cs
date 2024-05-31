using AMVTravels.Models.Entities;
using FluentValidation;

namespace AMVTravels.Validators
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {
            RuleFor(Reserv => Reserv)
                .NotEmpty()
                .NotNull()
                .WithMessage("Los Datos de la RESERVA no pueden ser nulos");

            RuleFor(x => x.ClientName)
                .NotEmpty().WithMessage("El campo NOMBRE CLIENTE no puede quedar vació, ingrese un valor")
                .NotNull().WithMessage("El campo NOMBRE CLIENTE no puede ser Nulo, ingrese un valor")
                .MaximumLength(200).WithMessage("El campo NOMBRE CLIENTE tiene un maximo de 200 caracteres");

            RuleFor(x => x.ReservationDate)
                .NotEmpty().WithMessage("El campo FECHA RESERVA no puede quedar vació, ingrese un valor")
                .NotNull().WithMessage("El campo FECHA RESERVA no puede ser Nulo, ingrese un valor");
        }
    }
}
