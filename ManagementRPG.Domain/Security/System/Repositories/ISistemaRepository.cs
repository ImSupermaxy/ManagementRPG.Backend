using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Security.System.Entities;
using ManagementRPG.Domain.Security.System.Queries;

namespace ManagementRPG.Domain.Security.System.Repositories
{
    public interface ISistemaRepository : IRepository<Sistema, int, int, SistemaQueryResult>
    {
    }
}
