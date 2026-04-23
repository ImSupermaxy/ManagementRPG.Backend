using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Abstractions.Commands.Updates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementRPG.Domain.Abstractions.Controllers
{
    public interface IController<TId, TInsert, TUpdate>
        where TInsert : ICommandInsert
        where TUpdate : ICommandUpdate<TId>
    {
        public ISender Sender { get; }
        protected abstract Task<IActionResult> GetAll();
        protected abstract Task<IActionResult> GetById(TId id);
        protected abstract Task<IActionResult> Post(TInsert command);
        protected abstract Task<IActionResult> Put(TUpdate command);
        //protected abstract Task<IActionResult> Delete(TId id);
    }

    public interface IController<TId, TUId, TInsert, TUpdate>
            : IController<TId, TInsert, TUpdate>
        where TInsert : ICommandInsert<TUId>
        where TUpdate : ICommandUpdate<TId, TUId>
    {
    }
}
