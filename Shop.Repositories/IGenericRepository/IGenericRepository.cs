using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.IGenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> SelectAll();
        T SelectById(long id);
        void Insert(T obj);
        Task Update(T obj);
        void Delete(object id);
        void DeleteByItem(T id);
        void Save();
    }
}
