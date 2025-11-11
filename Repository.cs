using GymCraftSolutionsWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODELS.Entities;
using System.Linq.Expressions;
using MODELS;
using DAL;

namespace SERVICES.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<TEntity> _entities;
        private bool _disposed = false;

        public Repository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = context.Set<TEntity>();
        }

        public TEntity GetById(int id, bool includeDeleted = false)
        {
            return includeDeleted ?
                    _entities.Find(id) :
                    _entities.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<TEntity> GetByIdAsync(int id, bool includeDeleted = false)
        {
            return includeDeleted ?
                    await _entities.FindAsync(id) :
                    await _entities.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public IQueryable<TEntity> GetAll(bool includeDeleted = false)
        {
            return includeDeleted ?
                    _entities.AsQueryable() :
                    _entities.Where(e => !e.IsDeleted);
        }
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false)
        {
            return includeDeleted ?
                    _entities.Where(predicate) :
                    _entities.Where(e => !e.IsDeleted).Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = false)
        {
            return includeDeleted ?
                    await _entities.ToListAsync() :
                    await _entities.Where(e => !e.IsDeleted).ToListAsync();
        }

        public int Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _entities.Add(entity);
            return _context.SaveChanges() > 0 ? entity.Id : 0;
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _entities.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0 ? entity.Id : 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _entities.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities));
            _entities.UpdateRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id, true);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _entities.Update(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~Repository()
        {
            Dispose(false);
        }
    }
}
