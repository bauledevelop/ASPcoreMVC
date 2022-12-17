using Shop.Entities.Enities;
using Shop.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.IRepositories
{
    public interface  IOrderDetailRepository
    {
        IQueryable<OrderDetail> SelectByIDOrder(long idOrder);
        IQueryable<OrderDetail> SelectByIDProduct(long id);
        IQueryable<OrderDetail> SelectAllOrderDetail();
        IEnumerable<OrderDetail> SelectAll();
        OrderDetail SelectById(long id);
        void Insert(OrderDetail obj);
        Task Update(OrderDetail obj);
        void Delete(object id);
        void DeleteByItem(OrderDetail id);
        void Save();
    }
}
