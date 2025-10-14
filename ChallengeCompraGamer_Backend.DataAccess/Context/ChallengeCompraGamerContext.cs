using System.Text;
using ChallengeCompraGamer_Backend.DataAccess.Configurations;
using ChallengeCompraGamer_Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChallengeCompraGamer_Backend.DataAccess.Context
{
    public class ChallengeCompraGamerContext : DbContext
    {
        public ChallengeCompraGamerContext(DbContextOptions<ChallengeCompraGamerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.ApplyConfiguration(new ChicoEntityConfiguration());
            // modelBuilder.ApplyConfiguration(new ChoferEntityConfiguration());
            // modelBuilder.ApplyConfiguration(new MicroEntityConfiguration());
        }
    }
}
