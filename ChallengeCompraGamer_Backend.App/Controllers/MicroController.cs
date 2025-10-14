using ChallengeCompraGamer_Backend.App.Commands.Micro.GetMicro;
using ChallengeCompraGamer_Backend.App.Commands.Micro.GetMicros;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCompraGamer_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MicroController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public MicroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetMicrosCommandRequest());
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return StatusCode((int) result.StatusCode, result.Message);
        }

        [HttpGet("{patente}")]
        public async Task<IActionResult> Get(string patente)
        {
            var query = new GetMicroCommandRequest(patente);
            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return StatusCode((int) result.StatusCode, result.Message);
        }
    }
}
