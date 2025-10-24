using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using MoneyTracker_API.Data;
using MoneyTracker_API.RepositoryContracts;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MoneyTracker_API.Repositroies
{
    public class Repository <T>: IRepositoryContracts<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbset; 
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this._dbset = _context.Set<T>();
        }
        public async Task<T> Create(T entity)
        {
            await _dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(T entity)
        {
            _dbset.Remove(entity);
            int rowsDeleted = await _context.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<T?> Get(Expression<Func<T, bool>> filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null, string? includePro = null)
        {
            IQueryable<T> query = _dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includePro != null)
            {
                foreach (var includeProp in includePro.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T?> Update(T entity)
        {
            _dbset.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
