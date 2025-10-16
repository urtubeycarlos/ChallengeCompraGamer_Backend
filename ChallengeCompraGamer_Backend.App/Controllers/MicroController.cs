using System.Net;
using ChallengeCompraGamer_Backend.App.Commands.Micro.AssignChicosToMicro;
using ChallengeCompraGamer_Backend.App.Commands.Micro.AssignChoferToMicro;
using ChallengeCompraGamer_Backend.App.Commands.Micro.CreateMicro;
using ChallengeCompraGamer_Backend.App.Commands.Micro.DeleteMicro;
using ChallengeCompraGamer_Backend.App.Commands.Micro.GetMicro;
using ChallengeCompraGamer_Backend.App.Commands.Micro.GetMicros;
using ChallengeCompraGamer_Backend.App.Commands.Micro.UpdateMicro;
using ChallengeCompraGamer_Backend.Models.Micro.AssignChicos;
using ChallengeCompraGamer_Backend.Models.Micro.AssignChofer;
using ChallengeCompraGamer_Backend.Models.Micro.Create;
using ChallengeCompraGamer_Backend.Models.Micro.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCompraGamer_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MicroController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public MicroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool incluirAsignados = false, [FromQuery] bool incluirCompletos = false)
        {
            GetMicrosCommand command = new GetMicrosCommand()
            {
                IncluirAsignados = incluirAsignados,
                IncluirCompletos = incluirCompletos
            };

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

        [HttpGet("{patente}")]
        public async Task<IActionResult> Get(string patente)
        {
            GetMicroCommand query = new GetMicroCommand(patente);
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
        public async Task<IActionResult> Post(CreateMicroRequestDTO request)
        {
            CreateMicroCommand command = new CreateMicroCommand() { Body = request };
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return StatusCode((int) HttpStatusCode.Created, result.Data);
            }
            else
            {
                return StatusCode((int)result.StatusCode, result.Message);
            }
        }

        [HttpPatch("{patente}")]
        public async Task<IActionResult> Patch(string patente, UpdateMicroRequestDTO request)
        {
            UpdateMicroCommand command = new UpdateMicroCommand() { Patente = patente, Body = request };
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

        [HttpDelete("{patente}")]
        public async Task<IActionResult> Delete(string patente)
        {
            DeleteMicroCommand query = new DeleteMicroCommand(patente);
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

        [HttpPut("{patente}/Chofer")]
        public async Task<IActionResult> AssignChoferToMicro(string patente, AssignChoferToMicroRequestDTO request)
        {
            AssignChoferToMicroCommand command = new AssignChoferToMicroCommand() { Patente = patente, Body = request };
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

        [HttpPut("{patente}/Chicos")]
        public async Task<IActionResult> AssignChicosToMicro(string patente, AssignChicosToMicroRequestDTO request)
        {
            AssignChicosToMicroCommand command = new AssignChicosToMicroCommand() { Patente = patente, Body = request };
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
    }
}
