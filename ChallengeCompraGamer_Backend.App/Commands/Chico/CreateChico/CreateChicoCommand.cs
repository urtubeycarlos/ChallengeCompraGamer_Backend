using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chico.Create;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Chico.CreateChico
{
    public class CreateChicoCommand : IRequest<Result<CreateChicoResponseDTO>>
    {
        public CreateChicoRequestDTO Body { get; set; }
    }
}
