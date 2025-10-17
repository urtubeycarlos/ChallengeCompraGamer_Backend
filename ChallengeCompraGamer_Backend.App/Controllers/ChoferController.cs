using System.Net;
using ChallengeCompraGamer_Backend.App.Commands.Chofer.CreateChofer;
using ChallengeCompraGamer_Backend.App.Commands.Chofer.DeleteChofer;
using ChallengeCompraGamer_Backend.App.Commands.Chofer.GetChofer;
using ChallengeCompraGamer_Backend.App.Commands.Chofer.GetChoferes;
using ChallengeCompraGamer_Backend.App.Commands.Chofer.UpdateChofer;
using ChallengeCompraGamer_Backend.Models.Chofer.Create;
using ChallengeCompraGamer_Backend.Models.Chofer.GetByDNI;
using ChallengeCompraGamer_Backend.Models.Chofer.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCompraGamer_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoferController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChoferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChoferes()
        {
            var result = await _mediator.Send(new GetChoferesCommand());
            
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return StatusCode((int)result.StatusCode, result.Message);
            }
        }

        [HttpGet("{dni}")]
        public async Task<IActionResult> GetChoferByDNI(string dni)
        {
            GetChoferByDniCommand query = new GetChoferByDniCommand(dni);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return StatusCode((int)result.StatusCode, result.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateChoferRequestDTO request)
        {
            CreateChoferCommand command = new CreateChoferCommand { Body = request };
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return StatusCode((int)HttpStatusCode.Created, result.Data);
            }
            else
            {
                return StatusCode((int)result.StatusCode, result.Message);
            }
        }

        [HttpPatch("{dni}")]
        public async Task<IActionResult> Patch(string dni, UpdateChoferRequestDTO request)
        {
            UpdateChoferCommand command = new UpdateChoferCommand() { DNI = dni, Body = request };
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return StatusCode((int)result.StatusCode, result.Message);
            }
        }

        [HttpDelete("{dni}")]
        public async Task<IActionResult> Delete(string dni)
        {
            DeleteChoferCommand query = new DeleteChoferCommand(dni);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return StatusCode((int)result.StatusCode, result.Message);
            }
        }
    }
}
