using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuraciones;

public class TipoMovimientoConfiguracion : IEntityTypeConfiguration<TipoMovimiento>
{
	public void Configure(EntityTypeBuilder<TipoMovimiento> builder)
	{
		builder
			.HasKey(x => x.Id);

		builder
			.Property(x => x.Id);

		builder
			.Property(x => x.Nombre)
			.IsRequired()
			.HasMaxLength(255);

		builder
			.Property(x => x.Descripcion)
			.IsRequired()
			.HasMaxLength(255);

		builder.ToTable("TipoMovimientos");
	}
}