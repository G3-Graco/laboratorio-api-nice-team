using Core.Entidades;
using FluentValidation;

namespace Services.Validadores
{
	public class PagoValidador : AbstractValidator<Pago>
	{
		public PagoValidador()
		{
			RuleFor(x => x.CuentaIdentificador)
				.NotEmpty();

			RuleFor(x => x.CuotaId)
				.NotEmpty();
		}
	}
}
