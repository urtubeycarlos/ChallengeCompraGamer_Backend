using System.ComponentModel.Design;
using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chico.GetByDNI;
using ChallengeCompraGamer_Backend.Services;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Chico.GetChico
{
    public class GetChicoByDniCommandHandler : IRequestHandler<GetChicoByDniCommand, Result<GetChicoByDniResponseDTO>>
    {
        private readonly ChicoService _service;
        private readonly ILogger<GetChicoByDniCommandHandler> _logger;

        public GetChicoByDniCommandHandler(ChicoService service, ILogger<GetChicoByDniCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<GetChicoByDniResponseDTO>> Handle(GetChicoByDniCommand command, CancellationToken cancellationToken)
        {
            try
            {
                GetChicoByDniResponseDTO chico = await _service.Get(command.DNI);
                return Result<GetChicoByDniResponseDTO>.Success(chico);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "No se encontró chico con DNI: {DNI}", command.DNI);
                return Result<GetChicoByDniResponseDTO>.Failure($"No se encontró chico con DNI: {command.DNI}", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error de servidor a intentar obtener chico con DNI: {DNI}", command.DNI);
                return Result<GetChicoByDniResponseDTO>.Failure($"Error de servidor a intentar obtener chico con DNI: {command.DNI}", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
