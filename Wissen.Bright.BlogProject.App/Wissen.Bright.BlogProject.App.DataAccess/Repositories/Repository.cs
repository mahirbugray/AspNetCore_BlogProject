using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wissen.Bright.BlogProject.App.DataAccess.Contexts;
using Wissen.Bright.BlogProject.App.Entity.Repositories;

namespace Wissen.Bright.BlogProject.App.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly BlogDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(BlogDbContext context)
        {
            _context = context;         //veritabanı
            _dbSet = _context.Set<T>(); //ilgili tablo
        }

        public async Task Add(T entity)
        {
            //_context.Set<T>().Add(entity);
             await _dbSet.AddAsync(entity);
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            this.Delete(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            //_dbSet.Entry().State = EntityState.Deleted;
            if(entity.GetType().GetProperty("IsDeleted") != null)
            {
                entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
                _dbSet.Update(entity);
                //this.Update(entity);   böyle de diyebiliriz.
            }
            else
            {
                _dbSet.Remove(entity);
            }

        }

        public async Task<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderby != null)
            {
                query = orderby(query);
            }
            foreach (var table in includes)
            {
                query = query.Include(table);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            if(orderby != null)
            {
                query = orderby(query);
            }
            foreach(var table in includes)
            {
                query = query.Include(table);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();   //Ef verileri takip (modified, deleted, detached gibi) etmiyor.
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }


    }
}
