using Asp.Versioning;
using ManagementRPG.Application.Global.Campanhas.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementRPG.API.Controllers.Global.Campanhas
{
    [ApiController]
    [ApiVersion(ApiVersions.Version)]
    [Route("api/v{version:apiVersion}/mestre/campanha")]
    public class CampanhaMestreController : ControllerBase
    {
        public ISender Sender { get; }

        public CampanhaMestreController(ISender sender)
            : base()
        {
            Sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Sender.Send(new CampanhaCommandGetByMestre(id));

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
    }
}
