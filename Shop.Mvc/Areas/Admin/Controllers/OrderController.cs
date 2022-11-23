using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using Shop.Mvc.Areas.Admin.Mapper;
using Shop.Mvc.Areas.Admin.Models;
using Shop.Mvc.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly IAccountBusiness _accountBusiness;
        public OrderController(IOrderBusiness orderBusiness, IAccountBusiness accountBusiness)
        {
            _orderBusiness = orderBusiness;
            _accountBusiness = accountBusiness;
        }

        [Area("Admin")]
        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 1)
        {
            try
            {
                var model = _orderBusiness.SelectByQuantityItem(page, pageSize);
                var orderViewModel = new List<OrderViewModel>();
                var mapperOrder = new OrderMapper();
                var mapperAccount = new AccountMapper();
                var pagination = new PaginationModel();
                var total = _orderBusiness.GetTotal();
                var listAccount = _accountBusiness.SelectAll();
                var accountViewModel = new List<AccountViewModel>();
                if (model != null)
                {
                    foreach (var item in listAccount)
                    {
                        accountViewModel.Add(mapperAccount.MapperDtoToViewModel(item));
                    }
                    foreach (var item in model)
                    {
                        orderViewModel.Add(mapperOrder.MapperDtoToViewModel(item));
                    }
                    pagination.Total = total;
                    pagination.Show = (total != 0 ? ((page - 1) * pageSize) + 1 : 0);
                    pagination.ShowTo = (((page - 1) * pageSize) + 1) + model.Count() - 1;
                    pagination.Page = page;
                    int maxPage = 5;
                    int totalPage = 0;
                    totalPage = (int)Math.Ceiling((double)((double)total / (double)pageSize));
                    pagination.TotalPage = totalPage;
                    pagination.MaxPage = 5;
                    pagination.First = 1;
                    pagination.Last = totalPage;
                    pagination.Next = page + 1;
                    pagination.Prev = page - 1;
                    ViewData["ListAccount"] = accountViewModel;
                    ViewData["Pagination"] = pagination;
                }
                return View(orderViewModel);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                _orderBusiness.DeleteOrder(long.Parse(id));
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}
