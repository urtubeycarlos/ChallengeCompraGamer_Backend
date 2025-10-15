using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro.AssignChicos;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.AssignChicosToMicro
{
    public class AssignChicosToMicroCommand : IRequest<Result<AssignChicosToMicroResponseDTO>>
    {
        public string Patente { get; set; }
        public AssignChicosToMicroRequestDTO Body { get; set; }
    }
}
