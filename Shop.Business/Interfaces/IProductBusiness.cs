using Shop.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces
{
    public interface IProductBusiness
    {
        void EditProduct(ProductDTO productDto);
        void CreateProduct(ProductDTO productDTO);
        ProductDTO SelectById(long id);
        void DeleteProduct(long id);
        long GetTotal();
        IEnumerable<ProductDTO> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<ProductDTO> SelectAll();

    }
}
