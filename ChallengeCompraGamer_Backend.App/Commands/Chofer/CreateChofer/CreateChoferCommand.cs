using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chofer.Create;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Chofer.CreateChofer
{
    public class CreateChoferCommand : IRequest<Result<CreateChoferResponseDTO>>
    {
        public CreateChoferRequestDTO Body { get; set; }
    }
}
