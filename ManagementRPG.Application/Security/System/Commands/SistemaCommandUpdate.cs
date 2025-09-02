using ManagementRPG.Domain.Abstractions.Commands.Updates;
using ManagementRPG.Domain.Security.System;

namespace ManagementRPG.Application.Security.System.Commands
{
    public sealed class SistemaCommandUpdate : SistemaGeneral, ICommandUpdate<int, int>
    {
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
