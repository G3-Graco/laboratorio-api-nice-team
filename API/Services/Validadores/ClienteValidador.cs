using Core.Entidades;
using FluentValidation;

namespace Services.Validadores
{
	public class ClienteValidador : AbstractValidator<Cliente>
	{
		public ClienteValidador()
		{
			RuleFor(x => x.Nombre)
                .NotEmpty()
				.MaximumLength(255);

			RuleFor(x => x.Apellido)
				.NotEmpty()
				.MaximumLength(255);

			RuleFor(x => x.Cedula)
				.NotEmpty()
				.MaximumLength(9)
				.MinimumLength(7)
				.Matches("^\\d{1,45}$");

			RuleFor(x => x.Correo)
				.NotEmpty()
				.MaximumLength(255);

			RuleFor(x => x.FechaNacimiento)
				.NotEmpty()
				.MaximumLength(255);

			RuleFor(x => x.Telefono)
				.NotEmpty()
				.MaximumLength(255);

			RuleFor(x => x.Direccion)
			.NotEmpty()
			.MaximumLength(255);
		}
	}
}
