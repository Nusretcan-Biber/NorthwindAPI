using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Utils.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public void Add(T model)
        {
            _dbSet.Add(model);
        }

        public void AddRange(List<T> model)
        {
            _dbSet.AddRange(model);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }

        public void Delete(T model, bool forceDelete = false)
        {
            // gelen modelin state'ini kontrol ediyoruz
            EntityEntry<T> dbEntityEntry = _dbContext.Entry(model);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                _dbSet.Attach(model);
                _dbSet.Remove(model);
            }
        }

        public void Delete(Expression<Func<T, bool>> predicate, bool forceDelete = false)
        {
            Delete(_dbSet.First(predicate), forceDelete);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = _dbSet
                .Where(predicate);
            return iQueryable.FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> iQueryable = _dbSet.Where(x => x != null);
            return iQueryable;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = _dbSet
                .Where(predicate);
            return iQueryable;
        }

        public DbContext GetDbContext()
        {
            return _dbContext;
        }

        public void Update(T model)
        {
            _dbSet.Attach(model);
            _dbContext.Entry(model).State = EntityState.Modified;

        }
        T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        T IRepository<T>.GetByIdiki(short id)
        {
            return _dbSet.Find(id);
        }
    }
}
