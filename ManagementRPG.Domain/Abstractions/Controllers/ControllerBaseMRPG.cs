using ManagementRPG.Domain.Shared.ApiConfig;
using ManagementRPG.Domain.Shared.ApiConfig.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ManagementRPG.Domain.Abstractions.Controllers
{
    public class ControllerBaseMRPG<TId> : ControllerBase
    {
        public ControllerBaseMRPG()
        {
        }

        protected TId GetUserId
        {
            get
            {
                var id = IdentifierTypeManager<TId>.ParseStringToTypeId(
                    User.Claims.FirstOrDefault(c => c.Type == TokenAuthConfig.UserIdentifier)!.Value
                );
                return id;
            }
        }

        protected string GetUserName
        {
            get
            {
                return User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(s => s.Value).FirstOrDefault() ?? default!;
            }
        }

        protected string GetUserEmail
        {
            get
            {
                return User.Claims.Where(c => c.Type == ClaimTypes.Email).Select(s => s.Value).FirstOrDefault() ?? default!;
            }
        }

        protected IEnumerable<string> GetUserRoles
        {
            get
            {
                return User.Claims.Where(c => c.Type == TokenAuthConfig.Role).Select(s => s.Value);
            }
        }
    }
}
