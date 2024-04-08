using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuraciones
{
    public class PrestamoConfiguracion : IEntityTypeConfiguration<Prestamo>
    {
        public void Configure(EntityTypeBuilder<Prestamo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.CuotaMensual).IsRequired();
            builder.Property(x => x.NumeroCuotas).IsRequired();
            builder.Property(x => x.MontoTotal).IsRequired();
            builder.Property(x => x.Fecha).IsRequired();
            builder.Property(x => x.IdCliente).IsRequired();
            builder.Property(x => x.IdEstado).IsRequired();
            builder.Property(x => x.IdPlazo).IsRequired();

            builder.HasOne(x => x.plazo).WithMany().HasForeignKey(x => x.IdPlazo);
            builder.HasOne(x => x.cliente).WithMany().HasForeignKey(x => x.IdCliente);
            builder.HasOne(x => x.Estado).WithMany().HasForeignKey(x => x.IdEstado);

            builder.ToTable("Prestamos");
        }
    }
}