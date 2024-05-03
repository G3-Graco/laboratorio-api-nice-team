using Core.Entidades;

namespace Core.Respuestas
{
	public class ModeloSolicitudPrestamo
	{
		public int NumeroCuotasDeseadas { get; set; }
		public double SueldoBasicoDelSolicitante { get; set; }
		public double MontoTotalDeseado { get; set; }
		public int IdClienteSolicitante { get; set; }

	}
}

