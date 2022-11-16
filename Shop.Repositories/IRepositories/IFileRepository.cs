using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.IRepositories
{
    public interface IFileRepository
    {
        long GetTotal();
        IEnumerable<File> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<File> SelectAll();
        File SelectById(long id);
        void Insert(File obj);
        Task Update(File obj);
        void Delete(object id);
        void DeleteByItem(File id);
        void Save();
    }
}
