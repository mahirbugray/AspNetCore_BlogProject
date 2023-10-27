using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wissen.Bright.BlogProject.App.Entity.Repositories;

namespace Wissen.Bright.BlogProject.App.Entity.UnitOfWorks
{
    public interface IUnitOfWorks : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, new();
        void Commit();          //İçine SaveChanges() gelecek.
        Task CommitAsync();
    }
}
