using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeCompraGamer_Backend.Models.Chofer.Update;
using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Services;

namespace ChallengeCompraGamer_Backend.App.Commands.Chofer.UpdateChofer
{
    public class UpdateChoferCommandHandler : IRequestHandler<UpdateChoferCommand, Result<UpdateChoferResponseDTO>>
    {
        private readonly ChoferService _service;
        private readonly ILogger<UpdateChoferCommandHandler> _logger;

        public UpdateChoferCommandHandler(ChoferService service, ILogger<UpdateChoferCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<UpdateChoferResponseDTO>> Handle(UpdateChoferCommand command, CancellationToken cancellationToken)
        {
            try
            {
                UpdateChoferResponseDTO result = await _service.Update(command.DNI, command.Body);
                return Result<UpdateChoferResponseDTO>.Success(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Chofer not found: {DNI}", command.DNI);
                return Result<UpdateChoferResponseDTO>.Failure(ex.Message, System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Chofer: {DNI}", command.DNI);
                return Result<UpdateChoferResponseDTO>.Failure("Error updating Chofer: " + ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
