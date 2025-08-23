using Asp.Versioning;
using ManagementRPG.Application.Commands.Campanhas;
using ManagementRPG.Domain.Abstractions.Controllers;
using ManagementRPG.Domain.Commands.Campanhas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementRPG.API.Controllers.Campanhas
{

    [ApiController]
    [ApiVersion(ApiVersions.Version)]
    [Route("api/v{version:apiVersion}/campanha")]
    public class CampanhaController : ControllerBase, IController<int, int, CampanhaCommandInsert, CampanhaCommandUpdate>
    {
        private readonly ISender _sender;

        public CampanhaController(ISender sender)
            : base()
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sender.Send(new CampanhaCommandGetAll());

            if (result.Success)
                return Ok(result);
            else
                return NotFound(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sender.Send(new CampanhaCommandGetById(id));

            if (result.Success)
                return Ok(result);
            else
                return NotFound(result);
        }

        [HttpPost]

        public async Task<IActionResult> Post(CampanhaCommandInsert command)
        {
            var result = await _sender.Send(command);

            if (result.Success)
                return Created();
            else
                return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(CampanhaCommandUpdate command)
        {
            var result = await _sender.Send(command);

            if (result.Success)
                return Created();
            else
                return BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sender.Send(new CampanhaCommandRemove(id));

            if (result.Success)
                return Ok();
            else
                return NotFound(result);
        }
    }
}