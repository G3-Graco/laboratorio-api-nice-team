using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuraciones;

public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id);

        builder
            .Property(x => x.NombreUsuario)
            .IsRequired()
            .HasMaxLength(255);

		builder
			.Property(x => x.Contrasena)
			.IsRequired()
			.HasMaxLength(255);


        builder.Property(x => x.ClienteId)
            .IsRequired();

        builder.ToTable("Usuarios");
    }
}