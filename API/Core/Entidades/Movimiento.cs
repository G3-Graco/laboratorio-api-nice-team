namespace Core.Entidades
{
	public class Movimiento
	{
		public int Id { get; set; }
		public double Monto { get; set; }
		public string Descripcion { get; set; }
		public DateTime Fecha { get; set; }
		public int TipoMovimientoId { get; set; }
		public virtual TipoMovimiento? TipoMovimiento { get; set; }
		public int CuentaOrigenIdentificador { get; set; }
		public int CuentaReceptoraIdentificador { get; set; }
	}
}
