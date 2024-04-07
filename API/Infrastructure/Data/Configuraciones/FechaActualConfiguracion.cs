using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuraciones;

public class FechaActualConfiguracion : IEntityTypeConfiguration<FechaActual>
{
	public void Configure(EntityTypeBuilder<FechaActual> builder)
	{
		//builder
		//	.HasKey(x => x.Id);

		//builder
		//	.Property(x => x.Id);

		builder
			.Property(x => x.Fecha)
			.IsRequired();


		builder.ToTable("FechaActual");
	}
}