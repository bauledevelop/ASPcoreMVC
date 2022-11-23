using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using Shop.Mvc.Entensions;
using Shop.Mvc.Models;

namespace Shop.Mvc.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IAccountBusiness _accountBusiness;
        public CheckoutController(IAccountBusiness accountBusiness)
        {
            _accountBusiness = accountBusiness;
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
    }
}
