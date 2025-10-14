using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chico.Delete;
using ChallengeCompraGamer_Backend.Services;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Chico.DeleteChico
{
    public class DeleteChicoCommandHandler : IRequestHandler<DeleteChicoCommand, Result<DeleteChicoResponseDTO>>
    {
        private readonly ChicoService _service;
        private readonly ILogger<DeleteChicoCommandHandler> _logger;

        public DeleteChicoCommandHandler(ChicoService service, ILogger<DeleteChicoCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<DeleteChicoResponseDTO>> Handle(DeleteChicoCommand command, CancellationToken cancellationToken)
        {
            try
            {
                DeleteChicoResponseDTO result = await _service.Delete(command.DNI);
                return Result<DeleteChicoResponseDTO>.Success(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Chico not found: {DNI}", command.DNI);
                return Result<DeleteChicoResponseDTO>.Failure("Chico not found", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting chico: {DNI}", command.DNI);
                return Result<DeleteChicoResponseDTO>.Failure("Error deleting chico", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
