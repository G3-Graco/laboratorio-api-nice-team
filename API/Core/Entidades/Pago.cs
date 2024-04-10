namespace Core.Entidades
{
	public class Pago
	{
		public int Id { get; set; }
		public int CuentaIdentificador { get; set; }
		public virtual Cuenta? CuentaOrigen { get; set; }
		public int CuotaId { get; set; }
		public virtual Cuota? CuotaPagada { get; set; }


	}

}
