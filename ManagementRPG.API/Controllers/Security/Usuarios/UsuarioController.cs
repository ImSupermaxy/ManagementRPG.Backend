using Asp.Versioning;
using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Domain.Abstractions.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ManagementRPG.API.Controllers.Security.Usuarios
{
    [ApiController]
    [ApiVersion(ApiVersions.Version)]
    [Route("api/v{version:apiVersion}/usuario")]
    //[Authorize(Roles = MasterONLY)]
    public class UsuarioController : ControllerBase, IController<int, int, UsuarioCommandInsert, UsuarioCommandUpdate>
    {
        public ISender Sender { get; }

        public UsuarioController(ISender sender)
        {
            Sender = sender;
        }

        #region Endpoints for DEVs

        [HttpGet]
        public Task<IActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Task<IActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsuarioCommandInsert command)
        {
            var result = await Sender.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result);

            var commandLogin = new UsuarioCommandLogin(command.Email, command.Senha, "");
            var resultLogin = await Sender.Send(command);

            if (!resultLogin.IsSuccess)
                return Unauthorized(resultLogin);

            return Ok();//new { Token = result.Value };
        }

        [HttpPut]
        public Task<IActionResult> Put(UsuarioCommandUpdate command)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
