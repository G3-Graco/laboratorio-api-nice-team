using Core.Entidades;
using FluentValidation;

namespace Services.Validadores
{
	public class MovimientoValidador : AbstractValidator<Movimiento>
	{
		public MovimientoValidador()
		{
			RuleFor(x => x.Monto)
				.NotEmpty();

			RuleFor(x => x.Descripcion)
				.NotEmpty()
				.MaximumLength(255);

			RuleFor(x => x.Fecha)
				.NotEmpty();

			RuleFor(x => x.TipoMovimientoId)
				.NotEmpty();
		}
	}
}