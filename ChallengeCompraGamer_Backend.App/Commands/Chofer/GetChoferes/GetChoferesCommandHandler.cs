using MediatR;
using ChallengeCompraGamer_Backend.Models.Chofer.GetAll;
using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Services;

namespace ChallengeCompraGamer_Backend.App.Commands.Chofer.GetChoferes
{
    public class GetChoferesCommandHandler : IRequestHandler<GetChoferesCommand, Result<IEnumerable<GetAllChoferesResponseDTO>>>
    {
        private readonly ChoferService _service;
        private readonly ILogger<GetChoferesCommandHandler> _logger;

        public GetChoferesCommandHandler(ChoferService service, ILogger<GetChoferesCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<GetAllChoferesResponseDTO>>> Handle(GetChoferesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<GetAllChoferesResponseDTO> result = await _service.GetAll();
                return Result<IEnumerable<GetAllChoferesResponseDTO>>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Choferes");
                return Result<IEnumerable<GetAllChoferesResponseDTO>>.Failure("Error retrieving Choferes: " + ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
