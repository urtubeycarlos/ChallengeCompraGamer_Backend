using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro.Delete;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.DeleteMicro
{
    public record DeleteMicroCommand(string Patente) : IRequest<Result<DeleteMicroResponseDTO>>;
}
