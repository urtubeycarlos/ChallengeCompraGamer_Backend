using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro.GetByPatente;
using ChallengeCompraGamer_Backend.Services;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.GetMicro
{
    public class GetMicroCommandHandler : IRequestHandler<GetMicroCommand, Result<GetMicroByPatenteResponseDTO>>
    {
        private readonly MicroService _service;
        private readonly ILogger<GetMicroCommandHandler> _logger;

        public GetMicroCommandHandler(MicroService service, ILogger<GetMicroCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<GetMicroByPatenteResponseDTO>> Handle(GetMicroCommand command, CancellationToken cancellationToken)
        {
            try
            {
                GetMicroByPatenteResponseDTO micro = await _service.Get(command.Patente);
                return Result<GetMicroByPatenteResponseDTO>.Success(micro);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Micro not found: {Patente}", command.Patente);
                return Result<GetMicroByPatenteResponseDTO>.Failure("Micro not found", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving micro: {Patente}", command.Patente);
                return Result<GetMicroByPatenteResponseDTO>.Failure("Error retrieving micro", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
