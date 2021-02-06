using Contest.Wallet.Common.Repository.SQL.Abstract;
using System;
using System.Threading.Tasks;

namespace Contest.Wallet.Common.Data.UOW.Abstract
{
    public interface IUnitOfWork<TDbContext> : IDisposable where TDbContext : IDbContext
    {
        /// <summary>
        /// Begins database transactions.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Saves database changes.
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Commits database transactions.
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Rolls back database transactions.
        /// </summary>
        Task RollbackAsync();
    }
}
