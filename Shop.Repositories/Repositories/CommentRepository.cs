using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly ShopContext _dbContext;
        public CommentRepository(ShopContext shopContext) : base(shopContext)
        {
            _dbContext = shopContext;
        }
        public IQueryable<Comment> SelectByIDProduct(long idProduct,int page,int pageSize)
        {
            try
            {
                var model = _dbContext.Comments.Where(x => x.Status == true && x.IDProduct == idProduct).Skip((page - 1)*pageSize).Take(pageSize);
                return model;
            }
            catch
            {
                return null;
            }
        }
        public long GetTotalByIDProduct(long productID)
        {
            return _dbContext.Comments.Where(x => x.Status == true && x.IDProduct == productID).Count();
        }
        public Comment GetCommentByIDAccount(long IDAccount)
        {
            try
            {
                var model = _dbContext.Comments.SingleOrDefault(x => x.IDAccount == IDAccount);
                return model;
            }
            catch {
                return null;
            }
        }
        public IQueryable<Comment> SelectByQuantityItem(int page, int pageSize)
        {
            try
            {
                var model = _dbContext.Comments.Where(x => x.ID != 0).Skip((page - 1) * pageSize).Take(pageSize);
                return model;
            }
            catch
            {
                return null;
            }
        }
        public long GetTotal()
        {
            return _dbContext.Comments.Where(x => x.ID!=0).Count();
        }
    }
}
