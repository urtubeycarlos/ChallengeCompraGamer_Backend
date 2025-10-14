using AutoMapper;
using ChallengeCompraGamer_Backend.DataAccess.Context;
using ChallengeCompraGamer_Backend.DataAccess.Entities;
using ChallengeCompraGamer_Backend.Models.Chico.Create;
using ChallengeCompraGamer_Backend.Models.Chico.Delete;
using ChallengeCompraGamer_Backend.Models.Chico.GetAll;
using ChallengeCompraGamer_Backend.Models.Chico.GetByDNI;
using ChallengeCompraGamer_Backend.Models.Chico.Update;
using Microsoft.EntityFrameworkCore;

namespace ChallengeCompraGamer_Backend.Services
{
    public class ChicoService
    {
        private readonly ChallengeCompraGamerContext _context;
        private readonly IMapper _mapper;

        public ChicoService(ChallengeCompraGamerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllChicosResponseDTO>> GetAll()
        {
            IEnumerable<Chico> chicos = await _context.Chicos.ToListAsync();
            return chicos.Select(_mapper.Map<GetAllChicosResponseDTO>);
        }

        public async Task<GetChicoByDniResponseDTO> Get(string dni)
        {
            Chico chico = await _context.Chicos
                                            .Include(c => c.Micro)
                                            .FirstOrDefaultAsync(c => c.DNI == dni);
            if (chico == null)
            {
                throw new KeyNotFoundException($"Chico with DNI {dni} not found.");
            }

            return _mapper.Map<GetChicoByDniResponseDTO>(chico);
        }

        public async Task<CreateChicoResponseDTO> Create(CreateChicoRequestDTO request)
        {
            if (await _context.Chicos.AnyAsync(c => c.DNI == request.DNI))
            {
                throw new InvalidOperationException($"Chico with DNI {request.DNI} already exists.");
            }

            Chico chico = _mapper.Map<Chico>(request);
            await _context.Chicos.AddAsync(chico);
            await _context.SaveChangesAsync();

            return _mapper.Map<CreateChicoResponseDTO>(chico);
        }

        public async Task<UpdateChicoResponseDTO> Update(string dni, UpdateChicoRequestDTO request)
        {
            Chico existingChico = await _context.Chicos.FindAsync(dni);
            if (existingChico == null)
            {
                throw new KeyNotFoundException($"Chico with DNI {dni} not found.");
            }

            _mapper.Map(request, existingChico);
            _context.Chicos.Update(existingChico);
            await _context.SaveChangesAsync();
            return _mapper.Map<UpdateChicoResponseDTO>(existingChico);
        }

        public async Task<DeleteChicoResponseDTO> Delete(string dni)
        {
            Chico chico = await _context.Chicos.FindAsync(dni);
            if (chico == null)
            {
                throw new KeyNotFoundException($"Chico with DNI {dni} not found.");
            }

            _context.Chicos.Remove(chico);
            await _context.SaveChangesAsync();
            return _mapper.Map<DeleteChicoResponseDTO>(chico);
        }
    }
}
