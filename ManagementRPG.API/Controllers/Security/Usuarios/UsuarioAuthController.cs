using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Domain.Abstractions.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace ManagementRPG.API.Controllers.Security.Usuarios
{
    [ApiController]
    [ApiVersion(ApiVersions.Version)]
    [Route("api/v{version:apiVersion}/auth")]
    public class UsuarioAuthController : ControllerBaseMRPG<int>
    {
        public ISender Sender { get; }

        public UsuarioAuthController(ISender sender)
        {
            Sender = sender;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = new UsuarioCommandLogin(request.Email, request.Password, request.TwoFactorCode);
            var result = await Sender.Send(command);

            if (result.IsFailure)
                return Unauthorized(result);

            return Ok(new { Token = result.Value });
        }

        [HttpPost("register")]
        [Authorize]
        public async Task<IActionResult> Register(UsuarioCommandRegister request)
        {
            //var id = GetUserId;
            var result = await Sender.Send(request);

            if (result.IsFailure)
                return BadRequest(result);

            return Ok(new { Token = result.Value });
        }

        [HttpPatch("inactivate")]
        public Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("activate")]
        public Task<IActionResult> Active(string email)
        {
            throw new NotImplementedException();
        }
    }
}
