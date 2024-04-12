using Core.Entidades;
using FluentValidation;

namespace Services.Validadores
{
	public class CuentaValidador : AbstractValidator<Cuenta>
	{
		public CuentaValidador()
		{
			RuleFor(x => x.Identificador)
				.NotEmpty()
				.GreaterThanOrEqualTo(100000000000);

			RuleFor(x => x.Saldo)
				.GreaterThanOrEqualTo(0);

			RuleFor(x => x.ClienteId)
				.NotEmpty();
		}
	}
}
