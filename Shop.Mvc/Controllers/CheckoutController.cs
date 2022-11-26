using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Mvc.Entensions;
using Shop.Mvc.Models;

namespace Shop.Mvc.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IAccountBusiness _accountBusiness;
        private readonly IOrderBusiness _orderBusiness;
        private readonly IOrderDetailBusiness _orderDetailBusiness;
        public CheckoutController(IAccountBusiness accountBusiness, IOrderBusiness orderBusiness, IOrderDetailBusiness orderDetailBusiness)
        {
            _accountBusiness = accountBusiness;
            _orderBusiness = orderBusiness;
            _orderDetailBusiness = orderDetailBusiness;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var _loginModel = HttpContext.Session.Get<LoginModel>("LoginModel");
                var accountDTO = _accountBusiness.GetAccountByUsername(_loginModel.Username);
                var _checkout = new CheckoutViewModel();
                var listCart = HttpContext.Session.Get<List<CartItem>>("ListCart");
                long total = 0;
                foreach(var item in listCart)
                {
                    total += item.TotalMoney;
                }
                _checkout.Email = accountDTO.Email;
                _checkout.Name = accountDTO.Name;
                _checkout.BirthDay = accountDTO.BirthDay;
                _checkout.Address = accountDTO.Address;
                _checkout.Phone = accountDTO.Phone;
                _checkout.Sex = ((accountDTO.Sex == 1) ? "Nam" : "Nữ");
                ViewBag.Total = total;
                ViewData["ListCart"] = listCart;
                return View(_checkout);
            }
            catch(Exception ex)
            {
                return Redirect("/404");
            }
        }
        [HttpGet]
        public IActionResult Payment()
        {
            try
            {
                var listCart = HttpContext.Session.Get<List<CartItem>>("ListCart");
                var loginModel = HttpContext.Session.Get<LoginModel>("LoginModel");
                long total = 0;
                long _quantity = 0;
                foreach(var item in listCart)
                {
                    total += item.TotalMoney;
                    _quantity += item.Amount;
                }
                var orderDTO = new OrderDTO();
                orderDTO.Total = total;
                orderDTO.Quantity = _quantity;
                orderDTO.IDAccount = loginModel.ID;
                var idOrder = _orderBusiness.InsertOrder(orderDTO);
                var orderDetaiDTO = new OrderDetailDTO();
                foreach(var item in listCart)
                {
                    orderDetaiDTO.IDOrder = idOrder;
                    orderDetaiDTO.IDProduct = item.Product.ID;
                    orderDetaiDTO.Quantity = item.Amount;
                    orderDetaiDTO.Color = int.Parse(item.Color);
                    orderDetaiDTO.Total = item.TotalMoney;
                    orderDetaiDTO.Size = int.Parse(item.Size);
                    _orderDetailBusiness.InsertOrderDetail(orderDetaiDTO);
                }
                listCart = null;
                HttpContext.Session.Set<List<CartItem>>("ListCart", listCart);
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                return Redirect("/404");
            }
        }

    }
}
