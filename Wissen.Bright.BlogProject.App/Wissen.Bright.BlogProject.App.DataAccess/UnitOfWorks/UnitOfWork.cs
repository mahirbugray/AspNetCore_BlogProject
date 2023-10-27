using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wissen.Bright.BlogProject.App.DataAccess.Contexts;
using Wissen.Bright.BlogProject.App.DataAccess.Repositories;
using Wissen.Bright.BlogProject.App.Entity.Repositories;
using Wissen.Bright.BlogProject.App.Entity.UnitOfWorks;

namespace Wissen.Bright.BlogProject.App.DataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWorks
    {
        private readonly BlogDbContext _context;
        private bool disposed = false;

        public UnitOfWork(BlogDbContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(_context);
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
