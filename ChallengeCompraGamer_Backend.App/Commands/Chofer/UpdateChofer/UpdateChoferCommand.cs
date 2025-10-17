using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Models.Chofer.Update;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCompraGamer_Backend.App.Commands.Chofer.UpdateChofer
{
    public class UpdateChoferCommand : IRequest<Result<UpdateChoferResponseDTO>>
    {
        public string DNI { get; set; }
        public UpdateChoferRequestDTO Body { get; set; }
    }
}
