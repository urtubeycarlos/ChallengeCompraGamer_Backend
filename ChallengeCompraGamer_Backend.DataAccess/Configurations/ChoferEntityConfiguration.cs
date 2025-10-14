using ChallengeCompraGamer_Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChallengeCompraGamer_Backend.DataAccess.Configurations
{
    public class ChoferEntityConfiguration : IEntityTypeConfiguration<Chofer>
    {
        public void Configure(EntityTypeBuilder<Chofer> builder)
        {
            builder.ToTable("chofer");
            builder.HasKey(c => c.DNI);

            builder.Property(c => c.DNI)
                .HasColumnName("dni")
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(c => c.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(c => c.Apellido)
                    .HasColumnName("apellido")
                    .IsRequired()
                    .HasMaxLength(128);

            builder.Property(c => c.Telefono)
                    .HasColumnName("telefono")
                    .IsRequired()
                    .HasMaxLength(16);

            builder.Property(c => c.ClaseLicencia)
                    .HasColumnName("clase_licencia")
                    .IsRequired()
                    .HasMaxLength(2);

            builder.Property(c => c.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime(3)") // precisión hasta milisegundos
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(3)") // incluye milisegundos
                    .IsRequired();

            builder.Property(c => c.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime(3)") // precisión hasta milisegundos
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                    .ValueGeneratedOnAddOrUpdate()
                    .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

            // Configuración de la relación uno a uno con Micro
            builder.HasOne(c => c.Micro)
                    .WithOne(m => m.Chofer)
                    .HasForeignKey<Micro>(m => m.ChoferDNI)
                    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
