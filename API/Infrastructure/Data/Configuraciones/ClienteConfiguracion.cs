using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuraciones;

public class ClienteConfiguracion : IEntityTypeConfiguration<Cliente>
{
	public void Configure(EntityTypeBuilder<Cliente> builder)
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
			.Property(x => x.Apellido)
			.IsRequired()
			.HasMaxLength(255);

		builder
			.Property(x => x.Cedula)
			.IsRequired()
			.HasMaxLength(255);

		builder
			.Property(x => x.Correo)
			.IsRequired()
			.HasMaxLength(255);

		builder
			.Property(x => x.FechaNacimiento)
			.IsRequired()
			.HasMaxLength(255);

		builder
			.Property(x => x.Telefono)
			.IsRequired()
			.HasMaxLength(255);

		builder
			.Property(x => x.Direccion)
			.IsRequired()
			.HasMaxLength(255);



		builder.ToTable("Clientes");
	}
}