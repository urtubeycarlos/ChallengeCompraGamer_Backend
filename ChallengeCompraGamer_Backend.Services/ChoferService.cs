using AutoMapper;
using ChallengeCompraGamer_Backend.DataAccess.Context;
using ChallengeCompraGamer_Backend.DataAccess.Entities;
using ChallengeCompraGamer_Backend.Models.Chofer.Create;
using ChallengeCompraGamer_Backend.Models.Chofer.Delete;
using ChallengeCompraGamer_Backend.Models.Chofer.GetAll;
using ChallengeCompraGamer_Backend.Models.Chofer.GetByDNI;
using ChallengeCompraGamer_Backend.Models.Chofer.Update;
using Microsoft.EntityFrameworkCore;

namespace ChallengeCompraGamer_Backend.Services
{
    public class ChoferService
    {
        private readonly ChallengeCompraGamerContext _context;
        private readonly IMapper _mapper;

        public ChoferService(ChallengeCompraGamerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllChoferesResponseDTO>> GetAll()
        {
            IEnumerable<Chofer> choferes = await _context.Choferes.ToListAsync();
            return _mapper.Map<IEnumerable<GetAllChoferesResponseDTO>>(choferes);
        }

        public async Task<GetChoferByDniResponseDTO> Get(string dni)
        {
            Chofer? chofer = await _context.Choferes
                                                .Include(c => c.Micro)
                                                .FirstOrDefaultAsync(c => c.DNI == dni);
            if (chofer == null)
            {
                throw new KeyNotFoundException($"Chofer with DNI {dni} not found.");
            }

            return _mapper.Map<GetChoferByDniResponseDTO>(chofer);
        }

        public async Task<CreateChoferResponseDTO> Create(CreateChoferRequestDTO request)
        {
            if (await _context.Choferes.AnyAsync(c => c.DNI == request.DNI))
            {
                throw new InvalidOperationException($"Chofer with DNI {request.DNI} already exists.");
            }

            Chofer chofer = _mapper.Map<Chofer>(request);
            await _context.Choferes.AddAsync(chofer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CreateChoferResponseDTO>(chofer);
        }

        public async Task<UpdateChoferResponseDTO> Update(string dni, UpdateChoferRequestDTO request)
        {
            Chofer existingChofer = await _context.Choferes.FirstOrDefaultAsync(c => c.DNI == dni);
            if (existingChofer == null)
            {
                throw new KeyNotFoundException($"Chofer with DNI {dni} not found.");
            }

            _mapper.Map(request, existingChofer);
            _context.Choferes.Update(existingChofer);
            await _context.SaveChangesAsync();
            return _mapper.Map<UpdateChoferResponseDTO>(existingChofer);
        }

        public async Task<DeleteChoferResponseDTO> Delete(string dni)
        {
            Chofer existingChofer = await _context.Choferes.FirstOrDefaultAsync(c => c.DNI == dni);
            if (existingChofer == null)
            {
                throw new KeyNotFoundException($"Chofer with DNI {dni} not found.");
            }

            _context.Choferes.Remove(existingChofer);
            await _context.SaveChangesAsync();
            return _mapper.Map<DeleteChoferResponseDTO>(existingChofer);
        }
    }
}
