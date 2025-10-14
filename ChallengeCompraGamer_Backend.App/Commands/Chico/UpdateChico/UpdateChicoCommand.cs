using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chico.Update;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Chico.UpdateChico
{
    public class UpdateChicoCommand : IRequest<Result<UpdateChicoResponseDTO>>
    {
        public string DNI { get; set; }
        public UpdateChicoRequestDTO Body { get; set; }
    }
}
