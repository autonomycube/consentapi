using Consent.Api.Auth.Data.Repositories.Abstract;
using Consent.Common.EnityFramework.DbContexts;
using Consent.Common.EnityFramework.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Consent.Api.Auth.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Private Variables

        private readonly ConsentIdentityDbContext _context;
        private DbSet<UserIdentity> _dbSet;

        #endregion

        #region Constructor

        public UserRepository(ConsentIdentityDbContext context)
        {
            _context = context
                 ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<UserIdentity>();
        }

        #endregion

        #region Public Methods

        public virtual UserIdentity GetById(string id)
        {
            return _dbSet.Where(t => t.Id.Equals(id)).FirstOrDefault();
        }

        public virtual async Task Update(UserIdentity entity)
        {
            if (entity.Equals(default(UserIdentity)))
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
