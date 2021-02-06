using Contest.Wallet.Common.Data.UOW.Abstract;
using Contest.Wallet.Common.Repository.SQL.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Contest.Wallet.Common.Data.UOW
{
    public class UnitOfWork<TDbContext>
        : IUnitOfWork<TDbContext> where TDbContext : IDbContext
    {
        #region Private Variables
        private readonly TDbContext _context;
        private IDbContextTransaction _transaction;
        private bool _disposed;
        #endregion

        #region Constructors
        public UnitOfWork(TDbContext context)
        {
            _context = context;
        }
        #endregion

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            if (this._transaction == null)
            {
                return;
            }

            await this.SaveChangesAsync();
            await this._transaction.CommitAsync();
        }

        public async Task SaveChangesAsync()
        {
            await this._context.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            if (this._transaction == null)
            {
                return;
            }

            await this._transaction.RollbackAsync();

            foreach (var entry in this._context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _transaction.Dispose();
                }
            }
            this._disposed = true;
        }
    }
}
