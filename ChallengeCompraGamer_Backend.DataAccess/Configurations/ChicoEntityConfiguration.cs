using ChallengeCompraGamer_Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChallengeCompraGamer_Backend.DataAccess.Configurations
{
    public class ChicoEntityConfiguration : IEntityTypeConfiguration<Chico>
    {
        public void Configure(EntityTypeBuilder<Chico> builder)
        {
            // Configuración de la entidad Chico
        }
    }
}
