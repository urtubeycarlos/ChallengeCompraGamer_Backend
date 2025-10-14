using ChallengeCompraGamer_Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChallengeCompraGamer_Backend.DataAccess.Configurations
{
    public class MicroEntityConfiguration : IEntityTypeConfiguration<Micro>
    {
        public void Configure(EntityTypeBuilder<Micro> builder)
        {
            builder.ToTable("micro");
            builder.HasKey(m => m.Patente);
            
            builder.Property(m => m.Patente)
                    .HasColumnName("patente")
                    .IsRequired()
                    .HasMaxLength(16);

            builder.Property(m => m.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime(3)") // precisión hasta milisegundos
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(3)") // incluye milisegundos
                    .IsRequired();

            builder.Property(m => m.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime(3)") // precisión hasta milisegundos
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                    .ValueGeneratedOnAddOrUpdate()
                    .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
        }
    }
}
