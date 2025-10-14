using ChallengeCompraGamer_Backend.DataAccess.Context;
using ChallengeCompraGamer_Backend.DataAccess.Entities;
using ChallengeCompraGamer_Backend.Models.Micro;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChallengeCompraGamer_Backend.Services
{
    public class MicroService
    {
        private readonly ChallengeCompraGamerContext _context;

        public MicroService(ChallengeCompraGamerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MicroDTO>> Get()
        {
            IEnumerable<MicroDTO> micros = await _context.Micros.Select(m => new MicroDTO
            {
                Patente = m.Patente,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt
            }).ToListAsync();

            return micros;
        }

        public async Task<MicroDTO> Get(string patente)
        {
            Micro micro = await _context.Micros.FindAsync(patente);
            if (micro == null)
            {
                throw new KeyNotFoundException($"Micro with patente {patente} not found.");
            }

            return new MicroDTO
            {
                Patente = micro.Patente,
                CreatedAt = micro.CreatedAt,
                UpdatedAt = micro.UpdatedAt
            };
        }
    }
}
