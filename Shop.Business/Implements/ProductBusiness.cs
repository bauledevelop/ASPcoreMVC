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
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IRateRepository _rateRepository;
        public ProductBusiness(IProductRepository productRepository, IMapper mapper,IFileBusiness fileBusiness
            , ICommentBusiness commentBusiness, IOrderDetailBusiness orderDetailBusiness, IOrderDetailRepository orderDetailRepository
            , IRateRepository rateRepository
            )
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _fileBusiness = fileBusiness;
            _commentBusiness = commentBusiness;
            _orderDetailBusiness = orderDetailBusiness;
            _orderDetailRepository = orderDetailRepository;
            _rateRepository = rateRepository;
        }
        public IEnumerable<ProductDTO> SelectByKeyWordQuantityItem(int page,int pageSize,string search)
        {
            var product = _productRepository.SelectByKeyWordQuantityItem(page, pageSize, search);
            var productDTO = product.Select(item => _mapper.Map<Product, ProductDTO>(item));
            return productDTO;
        }
        public IEnumerable<ProductDTO> SelectByKeyWord(string search)
        {
            var product = _productRepository.SelectByKeyWord(search);
            var productDTO = product.Select(item => _mapper.Map<Product, ProductDTO>(item));
            return productDTO;
        }
        public IEnumerable<ProductDTO> SelectOutStanding()
        {
            var products = _productRepository.SelectAllProduct();
            var productDTOs = products.Select(item => _mapper.Map<Product, ProductDTO>(item));
            var rate = _rateRepository.SelectAll();
            var standingDTOs = new List<StandingDTO>();
            foreach(var item in productDTOs)
            {
                var getRate = rate.Where(x => x.IDProduct == item.ID).Count();
                if (getRate != 0)
                {
                    var standingDTO = new StandingDTO();
                    standingDTO.productDTO = item;
                    standingDTO.amount = getRate;
                    standingDTOs.Add(standingDTO);
                }
            }
            var result = standingDTOs.OrderByDescending(x => x.amount).Take(4);
            var resultProductDTO = new List<ProductDTO>();
            foreach(var item in result)
            {
                resultProductDTO.Add(item.productDTO);
            }
            return resultProductDTO;
        }
        public IEnumerable<ProductDTO> SelectTrendProduct()
        {
            var products = _productRepository.SelectAllProduct();
            var orderDetails = _orderDetailRepository.SelectAllOrderDetail();
            var orderDetailDTOs = new List<OrderDetailDTO>();
            foreach(var item in products)
            {
                var orderDetail = new OrderDetailDTO();
                orderDetail.IDProduct = item.ID;
                orderDetail.Quantity = 0;
                foreach (var child in orderDetails.Where(x => x.Status == true  && x.IDProduct == item.ID))
                {
                    orderDetail.Quantity += child.Quantity;
                }
                if (orderDetail.Quantity != 0)
                {
                    orderDetailDTOs.Add(orderDetail);
                }
            }
            var result = orderDetailDTOs.OrderByDescending(x => x.Quantity).Take(8);
            var productDTO = new List<ProductDTO>();
            foreach (var item in result)
            {
                productDTO.Add(_mapper.Map<ProductDTO>(_productRepository.SelectById(item.IDProduct)));
            }
            return productDTO;  
        }
        public ProductDTO GetProductByID(long ID)
        {
            var product = _productRepository.GetByID(ID);
            var productDTO = _mapper.Map<Product, ProductDTO>(product);
            return productDTO;
        }
        public IEnumerable<ProductDTO> SelectRelatedProduct(long IDCategory,long ID)
        {
            var products = _productRepository.SelectRelatedProduct(IDCategory,ID);
            var productDTOs = products.Select(item => _mapper.Map<Product, ProductDTO>(item));
            return productDTOs;
        }
        public IEnumerable<ProductDTO> SelectNewProduct()
        {
            var products = _productRepository.SelectNewProduct();
            var productDTOs = products.Select(item => _mapper.Map<Product, ProductDTO>(item));
            return productDTOs;
        }
        public IEnumerable<ProductDTO> SelectByIDCategoryQuantityItem(int page,int pageSize,long IDCategory)
        {
            var products = _productRepository.SelectByIDCategoryQuantityItem(page, pageSize, IDCategory);
            var productDTOs = products.Select(item => _mapper.Map<Product, ProductDTO>(item));
            return productDTOs;
        }
        public long GetTotalByIDCategory(long IDCategory)
        {
            return _productRepository.GetTotalByIDCategory(IDCategory);
        }
        public IEnumerable<ProductDTO> SelectByIDCategory(long IDCategory)
        {
            var products = _productRepository.SelectByIDCategory(IDCategory);
            var productDTOs = products.Select(item => _mapper.Map<Product, ProductDTO>(item));
            return productDTOs;
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
        public long CreateProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<ProductDTO, Product>(productDTO);
            product.UpdatedDate = DateTime.Now;
            product.CreatedDate = DateTime.Now;
            product.IsDelete = false;
            _productRepository.Insert(product);
            _productRepository.Save();
            return product.ID;
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
