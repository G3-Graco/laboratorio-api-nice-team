using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuraciones
{
    public class CuotaConfiguracion : IEntityTypeConfiguration<Cuota>
    {
        public void Configure(EntityTypeBuilder<Cuota> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Pago).IsRequired();
            builder.Property(x => x.Fecha).IsRequired();
            builder.Property(x => x.FechaPago);
            builder.Property(x => x.IdPrestamo).IsRequired();
            builder.HasOne(x => x.prestamo).WithMany().HasForeignKey(x => x.IdPrestamo);

            builder.ToTable("Cuotas");
        }
    }
}