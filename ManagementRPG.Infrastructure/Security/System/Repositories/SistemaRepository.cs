using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Security.System.Entities;
using ManagementRPG.Domain.Security.System.Queries;
using ManagementRPG.Domain.Security.System.Repositories;

namespace ManagementRPG.Infrastructure.Security.System.Repositories
{
    public class SistemaRepository : Repository<Sistema, int, int, SistemaQueryResult>, ISistemaRepository
    {
        public SistemaRepository(IUnitOfWork uow, string numberTable, string customName = "001") 
            : base(uow, numberTable, customName)
        {
        }
    }
}
