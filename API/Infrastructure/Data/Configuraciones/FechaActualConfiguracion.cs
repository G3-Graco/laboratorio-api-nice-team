using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuraciones;

public class FechaActualConfiguracion : IEntityTypeConfiguration<FechaActual>
{
	public void Configure(EntityTypeBuilder<FechaActual> builder)
	{
		builder.HasNoKey(); //vaayalo

		builder
			.Property(x => x.Fecha)
			.IsRequired();


		builder.ToTable("FechaActual");
	}
}