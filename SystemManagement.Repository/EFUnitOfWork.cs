using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Common;

namespace SystemManagement.Repository
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public EFUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public DbTransaction Begin()
        {
            _context.Database.BeginTransaction();

            return _context.Database.CurrentTransaction.GetDbTransaction();
        }

        public async Task<DbTransaction> BeginAsync()
        {
            await _context.Database.BeginTransactionAsync();

            return _context.Database.CurrentTransaction.GetDbTransaction();
        }

        public void Commit()
        {
            _context.SaveChanges();
            _context.Database.CommitTransaction();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            _context.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }

        public async Task RollbackAsync()
        {
            _context.Database.RollbackTransaction();

            await Task.CompletedTask;
        }
    }
}
