using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Security.System;

namespace ManagementRPG.Application.Security.System.Commands
{
    public sealed class SistemaCommandInsert : SistemaGeneral, ICommandInsert<int>
    {
        public int UserId { get; set; }
    }
}
