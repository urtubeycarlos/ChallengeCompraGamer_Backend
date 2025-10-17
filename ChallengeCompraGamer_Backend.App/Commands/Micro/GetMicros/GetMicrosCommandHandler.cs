using System.Net;
using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro.GetAll;
using ChallengeCompraGamer_Backend.Services;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.GetMicros
{
    public class GetMicrosCommandHandler : IRequestHandler<GetMicrosCommand, Result<IEnumerable<GetAllMicrosResponseDTO>>>
    {
        private readonly MicroService _service;
        private readonly ILogger<GetMicrosCommandHandler> _logger;

        public GetMicrosCommandHandler(MicroService service, ILogger<GetMicrosCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<GetAllMicrosResponseDTO>>> Handle(GetMicrosCommand command, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<GetAllMicrosResponseDTO> micros = await _service.Get(command.IncluirAsignados, command.IncluirCompletos);
                return Result<IEnumerable<GetAllMicrosResponseDTO>>.Success(micros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving micros");
                return Result<IEnumerable<GetAllMicrosResponseDTO>>.Failure("Error retrieving micros", HttpStatusCode.InternalServerError);
            }
        }
    }
}
