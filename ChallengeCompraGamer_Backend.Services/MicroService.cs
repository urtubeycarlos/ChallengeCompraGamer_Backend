using AutoMapper;
using ChallengeCompraGamer_Backend.DataAccess.Context;
using ChallengeCompraGamer_Backend.DataAccess.Entities;
using ChallengeCompraGamer_Backend.Models.Micro.AssignChicos;
using ChallengeCompraGamer_Backend.Models.Micro.AssignChofer;
using ChallengeCompraGamer_Backend.Models.Micro.Create;
using ChallengeCompraGamer_Backend.Models.Micro.Delete;
using ChallengeCompraGamer_Backend.Models.Micro.GetAll;
using ChallengeCompraGamer_Backend.Models.Micro.GetByPatente;
using ChallengeCompraGamer_Backend.Models.Micro.Update;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ChallengeCompraGamer_Backend.Services
{
    public class MicroService
    {
        private readonly ChallengeCompraGamerContext _context;
        private readonly IMapper _mapper;

        public MicroService(ChallengeCompraGamerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllMicrosResponseDTO>> Get(bool incluirAsignados = false, bool incluirCompletos = false)
        {
            IEnumerable<Micro> micros = await _context.Micros
                                                            .Include(m => m.Chofer)
                                                            .Include(m => m.Chicos)
                                                            .ToListAsync();

            return micros
                    .Select(m => _mapper.Map<GetAllMicrosResponseDTO>(m))
                    .Where(m => incluirAsignados || !m.TieneChofer)
                    .Where(m => incluirCompletos || !m.EstaCompleto)
                    .ToList();
        }

        public async Task<GetMicroByPatenteResponseDTO> Get(string patente)
        {
            Micro micro = await _context.Micros
                                            .Include(m => m.Chofer)
                                            .Include(m => m.Chicos)
                                            .FirstOrDefaultAsync(m => m.Patente == patente);
            if (micro == null)
            {
                throw new KeyNotFoundException($"Micro with patente {patente} not found.");
            }

            return _mapper.Map<GetMicroByPatenteResponseDTO>(micro);
        }

        public async Task<CreateMicroResponseDTO> Create(CreateMicroRequestDTO request)
        {
            if (await _context.Micros.AnyAsync(m => m.Patente == request.Patente))
            {
                throw new InvalidOperationException($"Micro with patente {request.Patente} already exists.");
            }

            Micro micro = _mapper.Map<Micro>(request);
            _context.Micros.Add(micro);
            await _context.SaveChangesAsync();
            return _mapper.Map<CreateMicroResponseDTO>(micro);
        }

        public async Task<UpdateMicroResponseDTO> Update(string patente, UpdateMicroRequestDTO request)
        {
            Micro micro = await _context.Micros
                                            .Include(m => m.Chicos)
                                            .FirstOrDefaultAsync(m => m.Patente == patente);
            if (micro == null)
            {
                throw new KeyNotFoundException($"No se encontró micro con patente {patente}.");
            }

            int cantidadChicos = micro.Chicos?.Count ?? 0;
            if (cantidadChicos > request.CantidadAsientos)
            {
                throw new InvalidOperationException($"No se puede poner menos asientos que la cantidad de chicos ya asignados para el micro con patente {patente}");
            }

            _mapper.Map(request, micro);
            await _context.SaveChangesAsync();
            return _mapper.Map<UpdateMicroResponseDTO>(micro);
        }

        public async Task<DeleteMicroResponseDTO> Delete(string patente)
        {
            Micro micro = await _context.Micros.FindAsync(patente);
            if (micro == null)
            {
                throw new KeyNotFoundException($"Micro with patente {patente} not found.");
            }

            _context.Micros.Remove(micro);
            await _context.SaveChangesAsync();
            return _mapper.Map<DeleteMicroResponseDTO>(micro);
        }

        public async Task<AssignChicosToMicroResponseDTO> AssignChicosToMicro(string patente, AssignChicosToMicroRequestDTO request)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Micro micro = await _context.Micros
                                            .Include(m => m.Chicos)
                                            .Include(m => m.Chofer)
                                            .FirstOrDefaultAsync(m => m.Patente == patente);

                    if (micro == null)
                    {
                        throw new KeyNotFoundException($"Micro con patente {patente} no encontrada.");
                    }

                    // Si no se proporcionan DNIs, se limpian los chicos asignados
                    if (request == null || request.DNIS == null || !request.DNIS.Any())
                    {
                        micro.Chicos.Clear();
                        await _context.SaveChangesAsync();
                        await _context.Entry(micro).Collection(m => m.Chicos).LoadAsync();
                        await transaction.CommitAsync();
                       
                        return _mapper.Map<AssignChicosToMicroResponseDTO>(micro);
                    }

                    List<string> dnisUnicos = request.DNIS.Distinct().ToList();

                    // validación de capacidad

                    if (dnisUnicos.Count > micro.CantidadAsientos)
                    {
                        throw new InvalidOperationException($"No se pueden asignar {dnisUnicos.Count} chicos al micro {patente} que tiene capacidad para {micro.CantidadAsientos} asientos.");
                    }

                    List<Chico> nuevosChicos = await _context.Chicos
                                                                .Where(c => dnisUnicos.Contains(c.DNI))
                                                                .ToListAsync();

                    List<string> dnisEncontrados = nuevosChicos.Select(c => c.DNI).ToList();
                    List<string> dnisFaltantes = request.DNIS.Except(dnisEncontrados).ToList();

                    if (dnisFaltantes.Any())
                    {
                        throw new KeyNotFoundException($"Chicos con DNIs {string.Join(", ", dnisFaltantes)} no encontrados.");
                    }

                    // Para trackear correctamente debido al tipo de relación,
                    // primero limpiamos la lista actual y luego agregamos los nuevos chicos
                    micro.Chicos.Clear();
                    foreach (Chico chico in nuevosChicos)
                    {
                        micro.Chicos.Add(chico);
                    }

                    await _context.SaveChangesAsync();
                    await _context.Entry(micro).Collection(m => m.Chicos).LoadAsync();
                    await transaction.CommitAsync();

                    return _mapper.Map<AssignChicosToMicroResponseDTO>(micro);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<AssignChoferToMicroResponseDTO> AssingChoferToMicro(string patente, AssignChoferToMicroRequestDTO request)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Micro micro = await _context.Micros
                                            .Include(m => m.Chicos)
                                            .Include(m => m.Chofer)
                                            .FirstOrDefaultAsync(m => m.Patente == patente);
                    if (micro == null)
                    {
                        throw new KeyNotFoundException($"Micro con patente {patente} no encontrada.");
                    }

                    // Si no se proporciona DNI, se desasigna el chofer actual
                    if (request == null || string.IsNullOrEmpty(request.DNI))
                    {
                        micro.Chofer = null;
                        await _context.SaveChangesAsync();
                        await _context.Entry(micro).Reference(m => m.Chofer).LoadAsync();
                        await transaction.CommitAsync();

                        return _mapper.Map<AssignChoferToMicroResponseDTO>(micro);
                    }

                    Chofer chofer = await _context.Choferes.FirstOrDefaultAsync(c => c.DNI == request.DNI);
                    if (chofer == null)
                    {
                        throw new KeyNotFoundException($"Chofer con DNI {request.DNI} no encontrado.");
                    }

                    micro.Chofer = chofer;

                    await _context.SaveChangesAsync();
                    await _context.Entry(micro).ReloadAsync();
                    // await _context.Entry(micro).Reference(m => m.Chofer).LoadAsync();
                    await transaction.CommitAsync();
                
                    return _mapper.Map<AssignChoferToMicroResponseDTO>(micro);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
