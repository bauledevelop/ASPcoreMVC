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
        IEnumerable<ProductDTO> SelectOutStanding();
        IEnumerable<ProductDTO> SelectTrendProduct();
        ProductDTO GetProductByID(long ID);
        IEnumerable<ProductDTO> SelectRelatedProduct(long IDCategory,long ID);
        IEnumerable<ProductDTO> SelectNewProduct();
        IEnumerable<ProductDTO> SelectByIDCategoryQuantityItem(int page, int pageSize, long IDCategory);
        long GetTotalByIDCategory(long IDCategory);
        IEnumerable<ProductDTO> SelectByIDCategory(long IDCategory);
        void DeleteByIDAccount(long IDAccount);
        void DeleteByCategoryID(long ID);
        void EditProduct(ProductDTO productDto);
        void CreateProduct(ProductDTO productDTO);
        ProductDTO SelectById(long id);
        void DeleteProduct(long id);
        long GetTotal();
        IEnumerable<ProductDTO> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<ProductDTO> SelectAll();

    }
}
