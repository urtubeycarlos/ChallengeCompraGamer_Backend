using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chico.GetByDNI;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Chico.GetChico
{
    public record GetChicoByDniCommand(string DNI) : IRequest<Result<GetChicoByDniResponseDTO>>;
}
