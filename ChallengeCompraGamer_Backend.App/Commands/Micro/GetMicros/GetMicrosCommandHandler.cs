using System.Net;
using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro;
using ChallengeCompraGamer_Backend.Services;
using MediatR;
using Serilog;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.GetMicros
{
    public class GetMicrosCommandHandler : IRequestHandler<GetMicrosCommandRequest, Result<IEnumerable<MicroDTO>>>
    {
        private readonly MicroService _service;
        private readonly ILogger<GetMicrosCommandHandler> _logger;

        public GetMicrosCommandHandler(MicroService service, ILogger<GetMicrosCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<MicroDTO>>> Handle(GetMicrosCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<MicroDTO> micros = await _service.Get();
                return Result<IEnumerable<MicroDTO>>.Success(micros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving micros");
                return Result<IEnumerable<MicroDTO>>.Failure("Error retrieving micros", HttpStatusCode.InternalServerError);
            }
        }
    }
}
