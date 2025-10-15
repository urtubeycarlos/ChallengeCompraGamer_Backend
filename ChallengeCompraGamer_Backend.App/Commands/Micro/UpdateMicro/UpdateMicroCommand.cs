using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro.Update;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.UpdateMicro
{
    public class UpdateMicroCommand : IRequest<Result<UpdateMicroResponseDTO>>
    {
        public string Patente { get; set; }
        public UpdateMicroRequestDTO Body { get; set; }
    }
}
