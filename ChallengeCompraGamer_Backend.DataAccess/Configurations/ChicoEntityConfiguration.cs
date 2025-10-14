using ChallengeCompraGamer_Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChallengeCompraGamer_Backend.DataAccess.Configurations
{
    public class ChicoEntityConfiguration : IEntityTypeConfiguration<Chico>
    {
        public void Configure(EntityTypeBuilder<Chico> builder)
        {
            builder.ToTable("chico");
            builder.HasKey(c => c.DNI);

            builder.Property(c => c.DNI)
                    .HasColumnName("dni")
                    .IsRequired()
                    .HasMaxLength(16);

            builder.Property(c => c.Nombre)
                    .HasColumnName("nombre")
                    .IsRequired()
                    .HasMaxLength(128);

            builder.Property(c => c.Apellido)
                    .HasColumnName("apellido")
                    .IsRequired()
                    .HasMaxLength(128);

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

            // Configuración de la relación con Micro
            builder.Property(c => c.PatenteMicro)
                    .HasColumnName("patente_micro")
                    .HasMaxLength(16);

            builder.HasOne(c => c.Micro)
                    .WithMany(m => m.Chicos)
                    .HasForeignKey(c => c.PatenteMicro)
                    .HasConstraintName("FK_Chico_Micro")
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
