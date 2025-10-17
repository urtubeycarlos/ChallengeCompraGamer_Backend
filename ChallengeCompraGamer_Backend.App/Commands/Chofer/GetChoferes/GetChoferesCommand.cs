using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chofer.GetAll;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Chofer.GetChoferes
{
    public class GetChoferesCommand : IRequest<Result<IEnumerable<GetAllChoferesResponseDTO>>>
    {
    }
}
