using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro.Create;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.CreateMicro
{
    public class CreateMicroCommand : IRequest<Result<CreateMicroResponseDTO>>
    {
        public CreateMicroRequestDTO Body { get; set; }
    }
}
