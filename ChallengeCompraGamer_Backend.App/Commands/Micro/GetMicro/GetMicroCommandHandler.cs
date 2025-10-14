using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro;
using ChallengeCompraGamer_Backend.Services;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.GetMicro
{
    public class GetMicroCommandHandler : IRequestHandler<GetMicroCommandRequest, Result<MicroDTO>>
    {
        private readonly MicroService _service;
        private readonly ILogger<GetMicroCommandHandler> _logger;

        public GetMicroCommandHandler(MicroService service, ILogger<GetMicroCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<MicroDTO>> Handle(GetMicroCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                MicroDTO micro = await _service.Get(request.Patente);
                return Result<MicroDTO>.Success(micro);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Micro not found: {Patente}", request.Patente);
                return Result<MicroDTO>.Failure("Micro not found", System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving micro: {Patente}", request.Patente);
                return Result<MicroDTO>.Failure("Error retrieving micro", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
