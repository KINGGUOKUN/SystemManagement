using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace SystemManagement.Common
{
    public interface IUnitOfWork
    {
        DbTransaction Begin();

        Task<DbTransaction> BeginAsync();

        void Commit();

        Task CommitAsync();

        void Rollback();

        Task RollbackAsync();
    }
}
