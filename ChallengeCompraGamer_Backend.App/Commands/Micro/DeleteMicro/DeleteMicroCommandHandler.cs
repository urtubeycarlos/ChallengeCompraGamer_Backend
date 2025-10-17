using MediatR;
using ChallengeCompraGamer_Backend.Services;
using ChallengeCompraGamer_Backend.Models.Micro.Delete;
using ChallengeCompraGamer_Backend.Models;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.DeleteMicro
{
    public class DeleteMicroCommandHandler : IRequestHandler<DeleteMicroCommand, Result<DeleteMicroResponseDTO>>
    {
        private readonly MicroService _service;
        private readonly ILogger<DeleteMicroCommandHandler> _logger;

        public DeleteMicroCommandHandler(MicroService service, ILogger<DeleteMicroCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<DeleteMicroResponseDTO>> Handle(DeleteMicroCommand command, CancellationToken cancellationToken)
        {
            try
            {
                DeleteMicroResponseDTO response = await _service.Delete(command.Patente);
                return Result<DeleteMicroResponseDTO>.Success(response);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Micro not found: {Patente}", command.Patente);
                return Result<DeleteMicroResponseDTO>.Failure("Micro not found", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting micro: {Patente}", command.Patente);
                return Result<DeleteMicroResponseDTO>.Failure("Error deleting micro", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
