using System.Net;
using ChallengeCompraGamer_Backend.App.Commands.Chico.CreateChico;
using ChallengeCompraGamer_Backend.App.Commands.Chico.DeleteChico;
using ChallengeCompraGamer_Backend.App.Commands.Chico.GetChico;
using ChallengeCompraGamer_Backend.App.Commands.Chico.GetChicos;
using ChallengeCompraGamer_Backend.App.Commands.Chico.UpdateChico;
using ChallengeCompraGamer_Backend.Models.Chico.Create;
using ChallengeCompraGamer_Backend.Models.Chico.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCompraGamer_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChicoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChicoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetChicosCommand());

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
        public async Task<IActionResult> Get(string dni)
        {
            GetChicoByDniCommand query = new GetChicoByDniCommand(dni);
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
        public async Task<IActionResult> Post(CreateChicoRequestDTO request)
        {
            CreateChicoCommand command = new CreateChicoCommand() { Body = request };
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
        public async Task<IActionResult> Patch(string dni, UpdateChicoRequestDTO body)
        {
            UpdateChicoCommand command = new UpdateChicoCommand() { DNI = dni, Body = body };
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
            DeleteChicoCommand query = new DeleteChicoCommand(dni);
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
