using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeCompraGamer_Backend.Models.Chofer.GetByDNI;
using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Services;

namespace ChallengeCompraGamer_Backend.App.Commands.Chofer.GetChofer
{
    public class GetChoferByDniCommandHandler : IRequestHandler<GetChoferByDniCommand, Result<GetChoferByDniResponseDTO>>
    {
        private readonly ChoferService _service;
        private readonly ILogger<GetChoferByDniCommandHandler> _logger;

        public GetChoferByDniCommandHandler(ChoferService service, ILogger<GetChoferByDniCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<GetChoferByDniResponseDTO>> Handle(GetChoferByDniCommand command, CancellationToken cancellationToken)
        {
            try
            {
                GetChoferByDniResponseDTO result = await _service.Get(command.DNI);
                return Result<GetChoferByDniResponseDTO>.Success(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Chofer not found: {DNI}", command.DNI);
                return Result<GetChoferByDniResponseDTO>.Failure(ex.Message, System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Chofer: {DNI}", command.DNI);
                return Result<GetChoferByDniResponseDTO>.Failure("Error retrieving Chofer: " + ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
