using AutoMapper;
using Shop.Common.DTO;
using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Mapping
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            EntityToDto();
            DtoToEntity();
        }
        private void EntityToDto()
        {
            CreateMap<Account, AccountDTO>();
            CreateMap<CategoryProduct, CategoryProductDTO>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<Feedback, FeedbackDTO>();
            CreateMap<File, FileDTO>();
            CreateMap<OrderDetail, OrderDetailDTO>();
            CreateMap<Order, Order>();
            CreateMap<Payment, PaymentDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Menu, MenuDTO>();
            CreateMap<Rate, RateDTO>();
        }
        private void DtoToEntity()
        {
            CreateMap<AccountDTO, Account>();
            CreateMap<CategoryProductDTO, CategoryProduct>();
            CreateMap<CommentDTO, Comment>();
            CreateMap<FeedbackDTO, Feedback>();
            CreateMap<FileDTO, File>();
            CreateMap<OrderDetailDTO, OrderDetail>();
            CreateMap<OrderDTO, Order>();
            CreateMap<PaymentDTO, Payment>();
            CreateMap<ProductDTO, Product>();
            CreateMap<MenuDTO, Menu>();
            CreateMap<RateDTO, Rate>();
        }
    }
}
