using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuraciones
{
    public class DocumentoConfiguracion : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.documento).IsRequired();
            builder.Property(x => x.IdTipo).IsRequired();
            builder.Property(x => x.IdPrestamo).IsRequired();

            builder.HasOne(x => x.Tipo).WithMany().HasForeignKey(x => x.IdTipo);
            builder.HasOne(x => x.prestamo).WithMany().HasForeignKey(x => x.IdPrestamo);

            builder.ToTable("Documentos");
        }
    }
}