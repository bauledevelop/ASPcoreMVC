using Microsoft.EntityFrameworkCore;
using Shop.Entities.Enities;
using Shop.Repositories.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _db;
        protected readonly DbSet<T> _table;
        public GenericRepository(ShopContext dbContext)
        {
            _db = dbContext;
            _table = _db.Set<T>();
            
        }
        public IQueryable<T> SelectAll()
        {
            return _table.AsQueryable().AsNoTracking();
        }
        public T SelectById(long id)
        {
            return _table.Find(id);
        }
        public void Insert(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            _table.Add(obj);
        }
        public virtual Task Update(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            _db.ChangeTracker.Clear();
            _table.Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        public void Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            T existing = _table.Find(id);
            _table.Remove(existing);
        }
        public void DeleteByItem(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            _table.Remove(obj);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
