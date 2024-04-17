using Core.Respuestas;
using FluentValidation;

namespace Services.Validadores
{
	public class ModeloSolicitudPrestamoValidador : AbstractValidator<ModeloSolicitudPrestamo>
	{
		public ModeloSolicitudPrestamoValidador()
		{
			RuleFor(x => x.NumeroCuotasDeseadas)
				.NotEmpty();

			RuleFor(x => x.SueldoBasicoDelSolicitante)
				.NotEmpty();

			RuleFor(x => x.MontoTotalDeseado)
				.NotEmpty();

			RuleFor(x => x.IdClienteSolicitante)
				.NotEmpty();

			RuleFor(x => x.DocumentoIdentificacionPersonal)
				.NotEmpty();

			RuleFor(x => x.DocumentoComprobanteIngresos)
				.NotEmpty();

		}
	}
}
