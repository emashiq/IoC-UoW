using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.ModelConfiguration;
using Repository.Context;
using UnitOfWork;
using UnitOfWork.Repository;

namespace Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;
        private bool disposed = false;
        private Dictionary<Type, object> _repositories;
        public UnitOfWork() => _dbContext = _dbContext?? new AppContext();
        public int ExecuteSqlCommand(string sql, params object[] parameters) => _dbContext.Database.ExecuteSqlCommand(sql, parameters);

        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class => GetRepository<TEntity>().FromSql(sql, parameters);

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
                _repositories[type] = new Repository<TEntity>(_dbContext);
            return (IRepository<TEntity>)_repositories[type];
        }

        public object GetRepository(Type type)
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();
            if (!_repositories.ContainsKey(type))
            {
                Type myGeneric = typeof(IRepository<>);
                Type repositoryConstruction = myGeneric.MakeGenericType(type);
                object repositoryObject = Activator.CreateInstance(repositoryConstruction, _dbContext);
                _repositories[type] = repositoryObject;
            }
            return _repositories[type];
        }

        public int SaveChanges()
        {
            AuditEntityHandle();
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            AuditEntityHandle();
            return _dbContext.SaveChangesAsync();
        }

        private void AuditEntityHandle()
        {
            var addedAuditedEntities = _dbContext.ChangeTracker.Entries<IAuditableEntity>()
                .Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);
            var modifiedAuditedEntities = _dbContext.ChangeTracker.Entries<IAuditableEntity>()
                .Where(p => p.State == EntityState.Modified)
                .Select(p => p.Entity);
            var now  = DateTime.UtcNow;
            foreach (var added in addedAuditedEntities)
            {
                added.CreatedAt = now;
                added.LastModifiedAt = now;
            }
            foreach (var modified in modifiedAuditedEntities)
                modified.LastModifiedAt = now;
        }
        public async Task<int> SaveChangesAsync(params IUnitOfWork[] unitOfWorks)
        {
            using (var ts = _dbContext.Database.BeginTransaction())
            {
                var count = 0;
                foreach (var unitOfWork in unitOfWorks)
                    count += await unitOfWork.SaveChangesAsync();
                count += await SaveChangesAsync();
                ts.Commit();
                return count;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                {
                    _repositories?.Clear();
                    _dbContext.Dispose();
                }
            disposed = true;
        }
    }
}
