using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Micro.GetAll;
using MediatR;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.GetMicros
{
    public class  GetMicrosCommandRequest : IRequest<Result<IEnumerable<GetAllMicrosResponseDTO>>>
    {
        public bool IncluirAsignados { get; set; } = false;
        public bool IncluirCompletos { get; set; } = false;
    }
}
