using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuraciones;

public class PagoConfiguracion : IEntityTypeConfiguration<Pago>
{
	public void Configure(EntityTypeBuilder<Pago> builder)
	{
		builder
			.HasKey(x => x.Id);

		builder
			.Property(x => x.Id)
			.UseIdentityColumn();

		builder.Property(x => x.CuentaIdentificador)
			.IsRequired();

		builder.Property(x => x.CuotaId)
			.IsRequired();

		builder.HasOne(x => x.CuotaPagada).WithMany().HasForeignKey(x => x.CuotaId);
		builder.HasOne(x => x.CuentaOrigen).WithMany().HasForeignKey(x => x.CuentaIdentificador);

		builder.ToTable("Pagos");
	}
}