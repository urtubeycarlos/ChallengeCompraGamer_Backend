using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.GetMicros
{
    public record  GetMicrosCommandRequest : IRequest<Result<IEnumerable<MicroDTO>>>
    {
    }
}
