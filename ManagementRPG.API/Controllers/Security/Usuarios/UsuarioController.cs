using Asp.Versioning;
using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Domain.Abstractions.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementRPG.API.Controllers.Security.Usuarios
{
    [ApiController]
    [ApiVersion(ApiVersions.Version)]
    [Route("api/v{version:apiVersion}/usuario")]
    public class UsuarioController : ControllerBaseMRPG<int>, IController<int, int, UsuarioCommandInsert, UsuarioCommandUpdate>
    {
        public ISender Sender { get; }

        public UsuarioController(ISender sender)
        {
            Sender = sender;
        }

        #region Endpoints for DEVs

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Sender.Send(new UsuarioCommandGetAll());

            if (result.IsFailure)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(UsuarioCommandInsert command)
        {
            command.UserId = GetUserId;

            var result = await Sender.Send(command);
            if (result.IsFailure)
                return BadRequest(result);

            var commandLogin = new UsuarioCommandLogin(command.Email, command.Senha, "");
            var resultLogin = await Sender.Send(command);

            if (resultLogin.IsFailure)
                return Unauthorized(resultLogin);

            return Ok(resultLogin);//new { Token = result.Value };
        }

        [HttpPut]
        public Task<IActionResult> Put(UsuarioCommandUpdate command)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
