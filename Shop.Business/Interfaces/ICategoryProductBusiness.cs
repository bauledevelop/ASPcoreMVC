using Shop.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces
{
    public interface ICategoryProductBusiness
    {
        void DeleteByMenuID(long IDMenu);
        void DeleteByAccountID(long IDAccount);
        IEnumerable<CategoryProductDTO> SelectAllCategory();
        void DeleteCategory(long id);
        bool EditCategory(CategoryProductDTO categoryDto);
        CategoryProductDTO GetCategoryById(long id);
        bool InsertCategory(CategoryProductDTO categoryProductDTO);
        IEnumerable<CategoryProductDTO> SelectByQuantityItem(int page, int pageSize);
        long GetTotal();

    }
}
