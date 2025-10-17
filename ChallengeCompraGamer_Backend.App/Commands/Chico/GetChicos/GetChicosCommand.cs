using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chico.GetAll;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Chico.GetChicos
{
    public class GetChicosCommand : IRequest<Result<IEnumerable<GetAllChicosResponseDTO>>>
    {
    }
}
