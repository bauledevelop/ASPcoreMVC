using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using Shop.Mvc.Areas.Admin.Mapper;
using Shop.Mvc.Areas.Admin.Models;
using Shop.Mvc.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OrderController : Controller
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
        public IActionResult Index(int page = 1, int pageSize = 5)
        {
            try
            {
                var model = _orderBusiness.SelectByQuantityItem(page,pageSize);
                var orderViewModel = new List<OrderViewModel>();
                var mapperOrder = new OrderMapper();
                var mapperAccount = new AccountMapper();
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
                    ViewData["ListAccount"] = accountViewModel;
                    ViewData["Pagination"] = _orderBusiness.SelectAll().ToPagedList(page,pageSize);
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
        public IActionResult EditStatus(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                _orderBusiness.ChangeStatus(long.Parse(id));
                return Json(new
                {
                    status = true
                });
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    status = false
                });
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
