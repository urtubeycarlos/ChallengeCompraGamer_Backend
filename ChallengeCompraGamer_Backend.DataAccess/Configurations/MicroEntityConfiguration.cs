using ChallengeCompraGamer_Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChallengeCompraGamer_Backend.DataAccess.Configurations
{
    public class MicroEntityConfiguration : IEntityTypeConfiguration<Micro>
    {
        public void Configure(EntityTypeBuilder<Micro> builder)
        {
            throw new NotImplementedException();
        }
    }
}
