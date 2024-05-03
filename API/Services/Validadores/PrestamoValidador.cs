using Core.Entidades;
using FluentValidation;

namespace Services.Validadores
{
	public class PrestamoValidador : AbstractValidator<Prestamo>
	{
		public PrestamoValidador()
		{
			RuleFor(x => x.CuotaMensual);
			RuleFor(x => x.NumeroCuotas);
			RuleFor(x => x.MontoTotal);
			RuleFor(x => x.IdPlazo);
			RuleFor(x => x.IdEstado);
			RuleFor(x => x.Fecha);
			RuleFor(x => x.IdCliente);

		}
	}
}