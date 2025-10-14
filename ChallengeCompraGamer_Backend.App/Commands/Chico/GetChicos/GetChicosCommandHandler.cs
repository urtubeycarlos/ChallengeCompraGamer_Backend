using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chico.GetAll;
using ChallengeCompraGamer_Backend.Services;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Chico.GetChicos
{
    public class GetChicosCommandHandler : IRequestHandler<GetChicosCommand, Result<IEnumerable<GetAllChicosResponseDTO>>>
    {
        private readonly ChicoService _service;
        private readonly ILogger<GetChicosCommandHandler> _logger;

        public GetChicosCommandHandler(ChicoService service, ILogger<GetChicosCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<GetAllChicosResponseDTO>>> Handle(GetChicosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<GetAllChicosResponseDTO> chicos = await _service.GetAll();
                return Result<IEnumerable<GetAllChicosResponseDTO>>.Success(chicos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving chicos");
                return Result<IEnumerable<GetAllChicosResponseDTO>>.Failure("Error retrieving chicos", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
