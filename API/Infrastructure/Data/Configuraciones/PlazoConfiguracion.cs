using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuraciones
{
    public class PlazoConfiguracion : IEntityTypeConfiguration<Plazo>
    {
        public void Configure(EntityTypeBuilder<Plazo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Porcentaje).IsRequired();
            builder.Property(x => x.MinimoCuotas).IsRequired();
            builder.Property(x => x.MaximaCuotas).IsRequired();

            builder.ToTable("Plazos");
        }
    }
}