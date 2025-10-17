using MediatR;
using ChallengeCompraGamer_Backend.Models.Chofer.Delete;
using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ChallengeCompraGamer_Backend.App.Commands.Chofer.DeleteChofer
{
    public class DeleteChoferCommandHandler : IRequestHandler<DeleteChoferCommand, Result<DeleteChoferResponseDTO>>
    {
        private readonly ChoferService _service;
        private readonly ILogger<DeleteChoferCommandHandler> _logger;

        public DeleteChoferCommandHandler(ChoferService service, ILogger<DeleteChoferCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<DeleteChoferResponseDTO>> Handle(DeleteChoferCommand command, CancellationToken cancellationToken)
        {
            try
            {
                DeleteChoferResponseDTO result = await _service.Delete(command.DNI);
                return Result<DeleteChoferResponseDTO>.Success(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Chofer not found: {DNI}", command.DNI);
                return Result<DeleteChoferResponseDTO>.Failure("Chofer not found", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Chofer: {DNI}", command.DNI);
                return Result<DeleteChoferResponseDTO>.Failure("Error deleting Chofer", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
