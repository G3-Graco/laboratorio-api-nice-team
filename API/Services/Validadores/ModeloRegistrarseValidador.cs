using Core.Respuestas;
using FluentValidation;

namespace Services.Validadores
{
	public class ModeloRegistrarseValidador : AbstractValidator<ModeloRegistrarse>
	{
		public ModeloRegistrarseValidador()
		{
			RuleFor(x => x.NombreUsuario)
				.NotEmpty()
				.MaximumLength(255);

			RuleFor(x => x.Contrasena)
				.NotEmpty()
				.MaximumLength(255);

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
