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
                _logger.LogWarning(ex, "Chico already exists: {DNI}", command.Body.DNI);
                return Result<CreateChicoResponseDTO>.Failure("Chico already exists", System.Net.HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating chico: {DNI}", command.Body.DNI);
                return Result<CreateChicoResponseDTO>.Failure("Error creating chico", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
