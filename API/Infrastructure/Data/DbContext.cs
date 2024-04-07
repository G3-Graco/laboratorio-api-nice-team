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
	}

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
}
