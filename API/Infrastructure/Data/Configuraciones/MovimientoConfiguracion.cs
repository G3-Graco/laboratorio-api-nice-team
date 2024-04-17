using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuraciones;

public class MovimientoConfiguracion : IEntityTypeConfiguration<Movimiento>
{
	public void Configure(EntityTypeBuilder<Movimiento> builder)
	{
		builder
			.HasKey(x => x.Id);

		builder
			.Property(x => x.Id)
			.UseIdentityColumn(); 

		builder.Property(x => x.Monto)
			.IsRequired();

		builder
			.Property(x => x.Descripcion)
			.IsRequired()
			.HasMaxLength(255);

		builder
			.Property(x => x.Fecha)
			.IsRequired();

		builder.Property(x => x.TipoMovimientoId)
			.IsRequired();

		builder.Property(x => x.CuentaOrigenIdentificador);

		builder.Property(x => x.CuentaReceptoraIdentificador);

		builder.HasOne(x => x.TipoMovimiento).WithMany().HasForeignKey(x => x.TipoMovimientoId);

		builder.ToTable("Movimientos");
	}
}