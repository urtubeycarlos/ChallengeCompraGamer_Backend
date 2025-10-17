using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chico.Create;
using ChallengeCompraGamer_Backend.Services;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Chico.CreateChico
{
    public class CreateChicoCommandHandler : IRequestHandler<CreateChicoCommand, Result<CreateChicoResponseDTO>>
    {
        private readonly ChicoService _service;
        private readonly ILogger<CreateChicoCommandHandler> _logger;

        public CreateChicoCommandHandler(ChicoService service, ILogger<CreateChicoCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<CreateChicoResponseDTO>> Handle(CreateChicoCommand command, CancellationToken cancellationToken)
        {
            try
            {
                CreateChicoResponseDTO result = await _service.Create(command.Body);
                return Result<CreateChicoResponseDTO>.Success(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Chico con DNI {DNI} ya existe", command.Body.DNI);
                return Result<CreateChicoResponseDTO>.Failure($"Chico con DNI {command.Body.DNI} ya existe", System.Net.HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear chico con DNI: {DNI}", command.Body.DNI);
                return Result<CreateChicoResponseDTO>.Failure($"Error al crear chico con DNI: {command.Body.DNI}", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
