using MediatR;
using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro.Update;
using ChallengeCompraGamer_Backend.Services;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.UpdateMicro
{
    public class UpdateMicroCommandHandler : IRequestHandler<UpdateMicroCommand, Result<UpdateMicroResponseDTO>>
    {
        private readonly MicroService _service;
        private readonly ILogger<UpdateMicroCommandHandler> _logger;

        public UpdateMicroCommandHandler(MicroService service, ILogger<UpdateMicroCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<UpdateMicroResponseDTO>> Handle(UpdateMicroCommand command, CancellationToken cancellationToken)
        {
            try
            {
                UpdateMicroResponseDTO updatedMicro = await _service.Update(command.Patente, command.Body);
                return Result<UpdateMicroResponseDTO>.Success(updatedMicro);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Micro not found: {Patente}", command.Patente);
                return Result<UpdateMicroResponseDTO>.Failure("Micro not found", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating micro: {Patente}", command.Patente);
                return Result<UpdateMicroResponseDTO>.Failure("Error updating micro", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
