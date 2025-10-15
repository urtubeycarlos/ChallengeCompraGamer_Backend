using System.Net;
using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro.AssignChofer;
using ChallengeCompraGamer_Backend.Services;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.AssignChoferToMicro
{
    public class AssignChoferToMicroCommandHandler : IRequestHandler<AssignChoferToMicroCommand, Result<AssignChoferToMicroResponseDTO>>
    {
        private readonly MicroService _service;
        private readonly ILogger<AssignChoferToMicroCommandHandler> _logger;

        public AssignChoferToMicroCommandHandler(MicroService service, ILogger<AssignChoferToMicroCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<AssignChoferToMicroResponseDTO>> Handle(AssignChoferToMicroCommand command, CancellationToken cancellationToken)
        {
            try
            {
                AssignChoferToMicroResponseDTO response = await _service.AssingChoferToMicro(command.Patente, command.Body);
                return Result<AssignChoferToMicroResponseDTO>.Success(response);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return Result<AssignChoferToMicroResponseDTO>.Failure(ex.Message, HttpStatusCode.NotFound);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation while assigning Chofer to Micro: {Patente}, {DNI}", command.Patente, command.Body.DNI);
                return Result<AssignChoferToMicroResponseDTO>.Failure(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error asignando chofer {DNI} al micro {Patente}", command.Body.DNI, command.Patente);
                return Result<AssignChoferToMicroResponseDTO>.Failure($"Error asignando chofer {command.Body.DNI} al micro {command.Patente}", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
