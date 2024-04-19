﻿namespace Core.Entidades
{
	public class Movimiento
	{
		public int Id { get; set; }
		public double Monto { get; set; }
		public string? Descripcion { get; set; }
		public DateTime Fecha { get; set; }
		public int TipoMovimientoId { get; set; }
		public virtual TipoMovimiento? TipoMovimiento { get; set; }
		public Int64 CuentaOrigenIdentificador { get; set; }
		public Int64 CuentaReceptoraIdentificador { get; set; }
	}
}
