using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.GetMicro
{
    public record GetMicroCommandRequest(string Patente) : IRequest<Result<MicroDTO>>;
}
