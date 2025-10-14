using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chofer.GetByDNI;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Chofer.GetChofer
{
    public record GetChoferByDniCommand(string DNI) : IRequest<Result<GetChoferByDniResponseDTO>>;
}
