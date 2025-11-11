using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymCraftSolutionsWebAPI.Services.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        public TEntity GetById(int id, bool includeDeleted = false);
        public Task<TEntity> GetByIdAsync(int id, bool includeDeleted = false);
        public IQueryable<TEntity> GetAll(bool includeDeleted = false);
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false);
        public Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = false);
        public int Add(TEntity entity);
        public Task<int> AddAsync(TEntity entity);
        public Task<bool> UpdateAsync(TEntity entity);
        public Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities);
        public Task<bool> DeleteAsync(int id);
    }
}
