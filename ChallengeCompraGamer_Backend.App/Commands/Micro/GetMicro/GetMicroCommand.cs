using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro.GetByPatente;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.GetMicro
{
    public record GetMicroCommand(string Patente) : IRequest<Result<GetMicroByPatenteResponseDTO>>;
}
