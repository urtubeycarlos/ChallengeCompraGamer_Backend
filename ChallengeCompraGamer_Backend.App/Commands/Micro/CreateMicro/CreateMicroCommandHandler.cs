using MediatR;
using ChallengeCompraGamer_Backend.Services;
using ChallengeCompraGamer_Backend.Models.Micro.Create;
using ChallengeCompraGamer_Backend.Models;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.CreateMicro
{
    public class CreateMicroCommandHandler : IRequestHandler<CreateMicroCommand, Result<CreateMicroResponseDTO>>
    {
        private readonly MicroService _service;
        private readonly ILogger<CreateMicroCommandHandler> _logger;

        public CreateMicroCommandHandler(MicroService service, ILogger<CreateMicroCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<CreateMicroResponseDTO>> Handle(CreateMicroCommand command, CancellationToken cancellationToken)
        {
            try
            {
                CreateMicroResponseDTO micro = await _service.Create(command.Body);
                return Result<CreateMicroResponseDTO>.Success(micro);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Micro already exists: {Patente}", command.Body.Patente);
                return Result<CreateMicroResponseDTO>.Failure("Micro already exists", System.Net.HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating micro: {Patente}", command.Body.Patente);
                return Result<CreateMicroResponseDTO>.Failure("Error creating micro", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
