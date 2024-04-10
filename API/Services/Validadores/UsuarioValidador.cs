using Core.Entidades;
using FluentValidation;

namespace Services.Validadores
{
	public class UsuarioValidador : AbstractValidator<Usuario>
	{
		public UsuarioValidador()
		{
			RuleFor(x => x.Id)
				.NotEmpty();

			RuleFor(x => x.NombreUsuario)
				.NotEmpty()
				.MaximumLength(255);

			RuleFor(x => x.Contrasena)
				.NotEmpty()
				.MaximumLength(255);

			RuleFor(x => x.ClienteId)
				.NotEmpty();

		}
	}
}
