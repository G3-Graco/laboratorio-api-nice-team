using Core.Entidades;
using FluentValidation;

namespace Services.Validadores
{
	public class CuentaValidador : AbstractValidator<Cuenta>
	{
		public CuentaValidador()
		{
			RuleFor(x => x.Identificador)
				.NotEmpty();

			RuleFor(x => x.Saldo)
				.NotEmpty();

			RuleFor(x => x.ClienteId)
				.NotEmpty();
		}
	}
}
