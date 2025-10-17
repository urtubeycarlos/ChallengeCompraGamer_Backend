using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chico.Update;
using ChallengeCompraGamer_Backend.Services;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Chico.UpdateChico
{
    public class UpdateChicoCommandHandler : IRequestHandler<UpdateChicoCommand, Result<UpdateChicoResponseDTO>>
    {
        private readonly ChicoService _service;
        private readonly ILogger<UpdateChicoCommandHandler> _logger;

        public UpdateChicoCommandHandler(ChicoService service, ILogger<UpdateChicoCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<UpdateChicoResponseDTO>> Handle(UpdateChicoCommand command, CancellationToken cancellationToken)
        {
            try
            {
                UpdateChicoResponseDTO result = await _service.Update(command.DNI, command.Body);
                return Result<UpdateChicoResponseDTO>.Success(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Chico not found: {DNI}", command.DNI);
                return Result<UpdateChicoResponseDTO>.Failure("Chico not found", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating chico: {DNI}", command.DNI);
                return Result<UpdateChicoResponseDTO>.Failure("Error updating chico", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
