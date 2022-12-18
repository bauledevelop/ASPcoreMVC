using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using Shop.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        private readonly IAccountBusiness _accountBusiness;
        private readonly IOrderBusiness _orderBusiness;
        private readonly IProductBusiness _productBusiness;

        public HomeController(IAccountBusiness accountBusiness, IOrderBusiness orderBusiness, IProductBusiness productBusiness)
        {
            _accountBusiness = accountBusiness;
            _orderBusiness = orderBusiness;
            _productBusiness = productBusiness;
        }
        [Area("Admin")]
        public IActionResult Index()
        {
            try
            {
                ViewBag.Product = _productBusiness.CountProduct();
                ViewBag.Order = _orderBusiness.CountOrder();
                ViewBag.Account = _accountBusiness.CountAccount();
                ViewBag.Revenue = _orderBusiness.TotalOrder();
                return View();
            }
            catch(Exception ex)
            {
                return Redirect("/404");
            }
        }

        [HttpPost]
        [Area("Admin")]
        public JsonResult GetData()
        {
            try
            {
                var order = _orderBusiness.SelectAll();
                long ordered = 0;
                var total = order.Count();
                int percent = 0;
                if (total != 0)
                {
                    foreach (var item in order.Where(x => x.Status == true))
                    {
                        ordered++;
                    }   
                    percent = (int)Math.Round((double)(100 * ordered) / total);
                }
                else
                {
                    percent = 100;
                }
                var ListRevenue = new List<RevenueViewModel>
                {
                    new RevenueViewModel
                    {
                        Name = 1,
                        number= 0,
                    },
                    new RevenueViewModel
                    {
                        Name = 2,
                        number= 0,
                    },
                    new RevenueViewModel
                    {
                        Name = 3,
                        number= 0,
                    },
                    new RevenueViewModel
                    {
                        Name = 4,
                        number= 0,
                    },
                    new RevenueViewModel
                    {
                        Name = 5,
                        number= 0,
                    },
                    new RevenueViewModel
                    {
                        Name = 6,
                        number= 0,
                    },
                    new RevenueViewModel
                    {
                        Name = 7,
                        number= 0,
                    },
                    new RevenueViewModel
                    {
                        Name = 8,
                        number= 0,
                    },
                    new RevenueViewModel
                    {
                        Name = 9,
                        number= 0,
                    },
                    new RevenueViewModel
                    {
                        Name = 10,
                        number= 0,
                    },
                    new RevenueViewModel
                    {
                        Name = 11,
                        number= 0,
                    },
                    new RevenueViewModel
                    {
                        Name = 12,
                        number= 0,
                    },
                };
                foreach(var item in ListRevenue)
                {
                    item.number = _orderBusiness.RevenueMonth(item.Name);
                }
                return Json(new
                {
                    status = true,
                    listRevenue = ListRevenue,
                    percent = percent
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
    }
}
