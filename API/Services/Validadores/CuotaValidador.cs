using Core.Entidades;
using FluentValidation;

namespace Services.Validadores
{
    public class CuotaValidador : AbstractValidator<Cuota>
    {
        public CuotaValidador() {
            RuleFor(x => x.IdPrestamo);
            RuleFor(x => x.FechaPago);
            RuleFor(x => x.Fecha);
            RuleFor(x => x.Pago);
        }
    }
}