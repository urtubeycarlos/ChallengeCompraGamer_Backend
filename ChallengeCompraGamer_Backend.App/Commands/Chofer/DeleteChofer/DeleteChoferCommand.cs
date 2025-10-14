using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chofer.Delete;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Chofer.DeleteChofer
{
    public record DeleteChoferCommand(string DNI) : IRequest<Result<DeleteChoferResponseDTO>>;
}
