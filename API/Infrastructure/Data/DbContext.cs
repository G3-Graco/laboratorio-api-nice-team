using Core.Entidades;
using Infrastructure.Data.Configuraciones;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
	public DbSet<Cliente> Clientes { get; set; }
	public DbSet<Pago> Pagos { get; set; }
	public DbSet<Cuenta> Cuentas { get; set; }
	public DbSet<Movimiento> Movimientos { get; set; }
	public DbSet<TipoMovimiento> TipoMovimientos { get; set; }
	public DbSet<FechaActual> FechaActual { get; set; }
	public DbSet<Plazo> Plazos { get; set; }
 	public DbSet<Cuota> Cuotas { get; set; }
	public DbSet<Documento> Documentos { get; set; }
	public DbSet<TipoDocumento> TipoDocumentos { get; set; }
	public DbSet<Prestamo> Prestamos { get; set; }
	public DbSet<EstadoPrestamo> EstadoPrestamos { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UsuarioConfiguracion());
		builder.ApplyConfiguration(new ClienteConfiguracion());
		builder.ApplyConfiguration(new PagoConfiguracion());
		builder.ApplyConfiguration(new CuentaConfiguracion());
		builder.ApplyConfiguration(new MovimientoConfiguracion());
		builder.ApplyConfiguration(new TipoMovimientoConfiguracion());
		builder.ApplyConfiguration(new FechaActualConfiguracion());
		builder.ApplyConfiguration(new PlazoConfiguracion());
		builder.ApplyConfiguration(new CuotaConfiguracion());
		builder.ApplyConfiguration(new DocumentoConfiguracion());
		builder.ApplyConfiguration(new TipoDocumentoCofiguracion());
		builder.ApplyConfiguration(new PrestamoConfiguracion());
		builder.ApplyConfiguration(new EstadoPrestamoConfiguracion());
	}

}
