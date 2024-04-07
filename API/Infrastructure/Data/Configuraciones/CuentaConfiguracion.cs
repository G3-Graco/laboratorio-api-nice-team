using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuraciones;

public class CuentaConfiguracion : IEntityTypeConfiguration<Cuenta>
{
	public void Configure(EntityTypeBuilder<Cuenta> builder)
	{
		builder
			.HasKey(x => x.Identificador);

		builder
			.Property(x => x.Identificador);

		builder.Property(x => x.Saldo)
			.IsRequired();


		builder.Property(x => x.ClienteId)
			.IsRequired();

		builder.ToTable("Cuentas");
	}
}