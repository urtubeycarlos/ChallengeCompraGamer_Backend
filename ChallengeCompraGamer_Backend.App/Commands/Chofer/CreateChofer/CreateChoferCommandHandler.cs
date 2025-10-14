using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeCompraGamer_Backend.Models.Chofer.Create;
using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Services;

namespace ChallengeCompraGamer_Backend.App.Commands.Chofer.CreateChofer
{
    public class CreateChoferCommandHandler : IRequestHandler<CreateChoferCommand, Result<CreateChoferResponseDTO>>
    {
        private readonly ChoferService _service;
        private readonly ILogger<CreateChoferCommandHandler> _logger;

        public CreateChoferCommandHandler(ChoferService service, ILogger<CreateChoferCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<CreateChoferResponseDTO>> Handle(CreateChoferCommand command, CancellationToken cancellationToken)
        {
            try
            {
                CreateChoferResponseDTO result = await _service.Create(command.Body);
                return Result<CreateChoferResponseDTO>.Success(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Chofer already exists: {DNI}", command.Body.DNI);
                return Result<CreateChoferResponseDTO>.Failure(ex.Message, System.Net.HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Chofer: {DNI}", command.Body.DNI);
                return Result<CreateChoferResponseDTO>.Failure("Error creating Chofer: " + ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
