using AutoMapper;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Implements
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IFileBusiness _fileBusiness;
        private readonly ICommentBusiness _commentBusiness;
        private readonly IOrderDetailBusiness _orderDetailBusiness;
        public ProductBusiness(IProductRepository productRepository, IMapper mapper,IFileBusiness fileBusiness
            , ICommentBusiness commentBusiness, IOrderDetailBusiness orderDetailBusiness)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _fileBusiness = fileBusiness;
            _commentBusiness = commentBusiness;
            _orderDetailBusiness = orderDetailBusiness;
        }
        public void DeleteProduct(long ID)
        {
            _fileBusiness.DeleteByIDProduct(ID);
            _commentBusiness.DeleteByIDProduct(ID);
            _orderDetailBusiness.DeleteByIDProduct(ID);
            _productRepository.Delete(ID);
            _productRepository.Save();
        }
        public void DeleteByIDAccount(long IDAccount)
        {
            var product = _productRepository.SelectAll();
            foreach (var item in product)
            {
                if (item.CreatedBy == IDAccount)
                {
                    _fileBusiness.DeleteByIDProduct(item.ID);
                    _commentBusiness.DeleteByIDProduct(item.ID);
                    _orderDetailBusiness.DeleteByIDProduct(item.ID);
                    _productRepository.Delete(item.ID);
                    _productRepository.Save();
                }
            }
        }
        public void DeleteByCategoryID(long ID)
        {
            var product = _productRepository.SelectAll();
            if (product != null)
            {
                foreach (var item in product)
                {
                    if (item.IDCategoryProduct == ID)
                    {
                        _fileBusiness.DeleteByIDProduct(item.ID);
                        _commentBusiness.DeleteByIDProduct(item.ID);
                        _orderDetailBusiness.DeleteByIDProduct(item.ID);
                        _productRepository.DeleteByItem(item);
                        _productRepository.Save();
                    }
                }
            }
            
        }
        public void EditProduct(ProductDTO productDto)
        {
            var product = _mapper.Map<ProductDTO, Product>(productDto);
            product.UpdatedDate = DateTime.Now;
            _productRepository.Update(product);
            _productRepository.Save();
        }
        public void CreateProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<ProductDTO, Product>(productDTO);
            product.UpdatedDate = DateTime.Now;
            product.CreatedDate = DateTime.Now;
            _productRepository.Insert(product);
            _productRepository.Save();
        }
        public ProductDTO SelectById(long id)
        {
            var product = _productRepository.SelectById(id);
            var productDto = _mapper.Map<Product, ProductDTO>(product);
            return productDto;
        }
        public long GetTotal()
        {
            return _productRepository.GetTotal();
        }
        public IEnumerable<ProductDTO> SelectByQuantityItem(int page, int pageSize)
        {
            var products = _productRepository.SelectByQuantityItem(page, pageSize);
            var productDtos = products.Select(item => _mapper.Map<Product, ProductDTO>(item));
            return productDtos;
        }
        public IEnumerable<ProductDTO> SelectAll()
        {
            var products = _productRepository.SelectAll();
            var productDtos = products.Select(item => _mapper.Map<Product, ProductDTO>(item));
            return productDtos;
        }
    }
}
