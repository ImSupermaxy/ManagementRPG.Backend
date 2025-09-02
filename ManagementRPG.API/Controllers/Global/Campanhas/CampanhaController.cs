using Asp.Versioning;
using ManagementRPG.Application.Global.Campanhas.Commands;
using ManagementRPG.Domain.Abstractions.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementRPG.API.Controllers.Global.Campanhas
{

    [ApiController]
    [ApiVersion(ApiVersions.Version)]
    [Route("api/v{version:apiVersion}/campanha")]
    //[Authorize]
    public class CampanhaController : ControllerBase, IController<int, int, CampanhaCommandInsert, CampanhaCommandUpdate>
    {
        public ISender Sender { get; }

        public CampanhaController(ISender sender)
            : base()
        {
            Sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Sender.Send(new CampanhaCommandGetAll());

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Sender.Send(new CampanhaCommandGetById(id));

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CampanhaCommandInsert command)
        {
            var result = await Sender.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> Put(CampanhaCommandUpdate command)
        {
            var result = await Sender.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Created();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Sender.Send(new CampanhaCommandRemove(id));

            if (!result.Success)
                return NotFound(result);

            return Ok();
        }
    }
}