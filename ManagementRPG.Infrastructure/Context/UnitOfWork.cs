using ManagementRPG.Domain.Abstractions.Context;
using System.Data;

namespace ManagementRPG.Infrastructure.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDBContext Context { get; private set; }
        public IDbTransaction Transaction { get; private set; }

        public UnitOfWork(IDBContext context)
        {
            Context = context;
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
        {
            if (Transaction == null)
            {
                Transaction = Context.Connection.BeginTransaction(isolationLevel);
            }
            //else
            //{
            //    throw new OverflowException();
            //}
        }

        public bool CommitChanges()
        {
            try
            {
                Transaction?.Commit();
                DefaultOperation();

                return true;
            }
            catch (Exception ex)
            {
                DefaultOperation();
                return false;
            }
        }

        public bool Rollback()
        {
            try
            {
                Transaction?.Rollback();
                DefaultOperation();

                return true;
            }
            catch (Exception ex)
            {
                DefaultOperation();
                return false;
            }
        }

        private void DefaultOperation()
        {
            Transaction?.Dispose();
            Transaction = null;
        }
    }
}
