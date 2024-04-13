using Core.Entidades;

namespace Core.Respuestas
{
	public class ModeloSolicitudPrestamo
	{
		public int NumeroCuotasDeseadas { get; set; }
		public double SueldoBasicoDelSolicitante { get; set; }
		public double MontoTotalDeseado { get; set; }
		//public double CuotaMensual { get; set; }
		//public DateTime Fecha { get; set; }
		//public int IdEstadoPrestamo { get; set; }
		public int IdClienteSolicitante { get; set; }
		//public int IdPlazo { get; set; }
		public byte[] DocumentoIdentificacionPersonal { get; set; }
		public byte[] DocumentoComprobanteIngresos { get; set; }
	}
}

