using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using Shop.Entities.Enities;
using Shop.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailBusiness _orderDetailBusiness;
        private readonly IProductBusiness _productBusiness;
        public OrderDetailController(IOrderDetailBusiness orderDetailBusiness, IProductBusiness productBusiness)
        {
            _orderDetailBusiness = orderDetailBusiness;
            _productBusiness = productBusiness;
        }

        [Area("Admin")]
        [HttpPost]
        public JsonResult GetDataByIDOrder(string IDOrder)
        {
            if (string.IsNullOrEmpty(IDOrder)) throw new ArgumentNullException(nameof(IDOrder));
            try
            {
                var orderDetailDTO = _orderDetailBusiness.SelectByIDOrder(long.Parse(IDOrder));
                var productDTO = _productBusiness.SelectAll();
                var orderDetailViewModel = new List<OrderDetailViewModel>();
                foreach(var item in orderDetailDTO)
                {
                    var orderDetail = new OrderDetailViewModel();
                    orderDetail.Name = productDTO.SingleOrDefault(x => x.ID == item.IDProduct).Name;
                    orderDetail.Color = (item.Color == 1 ? "Black" : "White");
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Total = item.Total;
                    orderDetail.Size = (item.Size == 1 ? "S" : item.Size == 2 ? "M" : "L");
                    orderDetailViewModel.Add(orderDetail);
                }
                return Json(new
                {
                    status = true,
                    data = orderDetailViewModel
                }) ;
            }
            catch(Exception ex) {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}
