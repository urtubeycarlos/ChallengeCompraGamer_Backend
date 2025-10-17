using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chico.Delete;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Chico.DeleteChico
{
    public record DeleteChicoCommand(string DNI) : IRequest<Result<DeleteChicoResponseDTO>>;
}
