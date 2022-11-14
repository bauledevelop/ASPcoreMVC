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
        public ProductBusiness(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
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
        public void DeleteProduct(long id)
        {
            _productRepository.Delete(id);
            _productRepository.Save();
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
