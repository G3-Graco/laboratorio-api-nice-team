using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuraciones
{
    public class TipoDocumentoCofiguracion : IEntityTypeConfiguration<TipoDocumento>
    {
        public void Configure(EntityTypeBuilder<TipoDocumento> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Descripcion).HasMaxLength(225);

            builder.ToTable("TiposDocumentos");
        }
    }
}