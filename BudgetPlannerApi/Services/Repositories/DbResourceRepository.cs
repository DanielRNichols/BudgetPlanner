using BudgetPlanner.Data;
using BudgetPlannerApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Services.Repositories
{
    public class DbResourceRepository<T, O> : 
        IDbResourceRepository<T, O> where T: class, IDbResource where O: class, IBaseQueryOptions
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _dbContext;
        public DbResourceRepository(ApplicationDbContext db, DbSet<T> dbContext)
        {
            _db = db;
            _dbContext = dbContext;
        }

        public virtual async Task<bool> Create(T entity)
        {
            await _dbContext.AddAsync(entity);

            return await Save();
        }

        public virtual async Task<bool> Delete(T entity)
        {
            _dbContext.Remove(entity);

            return await Save();
        }

        public virtual async Task<bool> Exists(int id)
        {
            return await _dbContext.AnyAsync(row => row.Id == id);
        }

        public virtual async Task<IList<T>> Get(O options = null)
        {
            // Override if you need to include related objects
            var query = _dbContext.AsQueryable();

            return await ExecuteQuery(query, options);
        }

        public virtual async Task<T> GetById(int id, IBaseQueryOptions options)
        {
            // Override if you need to include related objects
            var query = _dbContext.AsQueryable();

            return await ExecuteQueryById(id, query, options);
        }

        public virtual async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();

            return changes > 0;
        }

        public virtual async Task<bool> Update(T entity)
        {
            _dbContext.Update(entity);

            return await Save();
        }


        public virtual async Task<IList<T>> ExecuteQuery(IQueryable<T> query, IBaseQueryOptions options)
        {
            if (query == null || options == null)
                return null;

            // options.UserId is added in the controller for current user
            query = query.Where(r => r.UserId == options.UserId);

            if (options.MarkedForDeletion != null)
                query = query.Where(r => r.MarkedForDeletion == options.MarkedForDeletion);
            if (options.Skip > 0)
                query = query.Skip(options.Skip);
            if (options.Limit > 0)
                query = query.Take(options.Limit);

            return await query.ToListAsync();
        }

        public virtual async Task<T> ExecuteQueryById(int id, IQueryable<T> query, IBaseQueryOptions options)
        {
            if (query == null || options == null)
                return null;

            // options.UserId is added in the controller for current user
            query = query.Where(r => r.UserId == options.UserId);

            return await query.FirstOrDefaultAsync(q => q.Id == id);
        }


    }
}
