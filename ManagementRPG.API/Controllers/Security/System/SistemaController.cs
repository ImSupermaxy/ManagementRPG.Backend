using Asp.Versioning;
using ManagementRPG.Application.Security.System.Commands;
using ManagementRPG.Domain.Abstractions.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementRPG.API.Controllers.Security.System
{
    [ApiController]
    [ApiVersion(ApiVersions.Version)]
    [Route("api/v{version:apiVersion}/sistema")]
    //[Authorize(Roles = MasterONLY)]
    public class SistemaController : ControllerBase, IController<int, int, SistemaCommandInsert, SistemaCommandUpdate>
    {
        public ISender Sender { get; }

        public SistemaController(ISender sender)
        {
            Sender = sender;
        }

        #region Endpoints for DEVs

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Sender.Send(new SistemaCommandGetAll());

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Sender.Send(new SistemaCommandGetById(id));

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SistemaCommandInsert command)
        {
            var result = await Sender.Send(command);

            if (result.IsFailure)
                return BadRequest(result);

            return Created(string.Empty, result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Put(SistemaCommandUpdate command)
        {
            var result = await Sender.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Created();
        }

        #endregion
    }
}
