using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Abstractions.Commands.Updates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementRPG.Domain.Abstractions.Controllers
{
    public interface IController<TId, TCommandInsert, TCommandUpdate>
        where TCommandInsert : ICommandInsert
        where TCommandUpdate : ICommandUpdate<TId>
    {
        public ISender Sender { get; }
        protected abstract Task<IActionResult> GetAll();
        protected abstract Task<IActionResult> GetById(TId id);
        protected abstract Task<IActionResult> Post(TCommandInsert command);
        protected abstract Task<IActionResult> Put(TCommandUpdate command);
        //protected abstract Task<IActionResult> Delete(TId id);
    }

    public interface IController<TId, TUId, TCommandInsert, TCommandUpdate>
            : IController<TId, TCommandInsert, TCommandUpdate>
        where TCommandInsert : ICommandInsert<TUId>
        where TCommandUpdate : ICommandUpdate<TId, TUId>
    {
    }
}
