using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementRPG.Domain.Shared.ApiConfig.Authentication
{
    public interface IAppSettings
    {
        string Secret { get; init; }
        int ExpirationHours { get; init; }
        string Sender { get; init; }
        string ValidAt { get; init; }
    }
}
