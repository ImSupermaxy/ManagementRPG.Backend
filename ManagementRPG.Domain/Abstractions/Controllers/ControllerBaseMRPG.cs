using ManagementRPG.Domain.Shared.ApiConfig;
using ManagementRPG.Domain.Shared.ApiConfig.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
                var id = ConvertStringToTId(User.Claims.FirstOrDefault(c => c.Type == TokenAuthConfig.UserIdentifier)!.Value);
                return id;
            }
        }

        protected string GetUserName
        {
            get
            {
                return User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(s => s.Value).FirstOrDefault();
            }
        }

        protected string GetUserEmail
        {
            get
            {
                return User.Claims.Where(c => c.Type == ClaimTypes.Email).Select(s => s.Value).FirstOrDefault();
            }
        }

        protected IEnumerable<string> GetUserRoles
        {
            get
            {
                return User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(s => s.Value);
            }
        }

        private TId ConvertStringToTId(string id)
        {
            var result = IdentifierTypeManager<TId>.IsValidTypeIdentifier();
            if (!result)
                throw new Exception("Type of identity is invalid");
            return (TId)(id as object);
        }
    }
}
