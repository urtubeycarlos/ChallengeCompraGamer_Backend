using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro.AssignChofer;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.AssignChoferToMicro
{
    public class AssignChoferToMicroCommand : IRequest<Result<AssignChoferToMicroResponseDTO>>
    {
        public string Patente { get; set; }
        public AssignChoferToMicroRequestDTO Body { get; set; }
    }
}
