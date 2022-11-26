using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using Shop.Mvc.Entensions;
using Shop.Mvc.Models;
using System.Diagnostics;

namespace Shop.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountBusiness _accountBusiness;
        private readonly IProductBusiness _productBusiness;
        private readonly IFileBusiness _fileBusiness;
        public HomeController(IAccountBusiness accountBusiness, IProductBusiness productBusiness, IFileBusiness fileBusiness)
        {
            _accountBusiness = accountBusiness;
            _productBusiness = productBusiness;
            _fileBusiness = fileBusiness;
        }
        public IActionResult Index()
        {
            try
            {
                var listNew = _productBusiness.SelectNewProduct();
                var listFile = _fileBusiness.SelectAll();
                var listTrend = _productBusiness.SelectTrendProduct();
                var listOutStanding = _productBusiness.SelectOutStanding();
                ViewData["ListTrend"] = listTrend;
                ViewData["ListNew"] = listNew;
                ViewData["ListFile"] = listFile;
                ViewData["ListOutStanding"] = listOutStanding;
                return View();
            }
            catch (Exception ex)
            {
                return Redirect("/404");
            }

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (loginModel.Username == null)
            {
                ViewBag.Message = "Vui lòng nhập username";
                return View(loginModel);
            }
            else
            {
                if (loginModel.Password == null)
                {
                    ViewBag.Message = "Vui lòng nhập mật khẩu";
                    return View(loginModel);
                }
                else
                {
                    var check = _accountBusiness.CheckLogin(loginModel.Username, loginModel.Password);
                    if (check == 1)
                    {
                        ViewBag.Message = "Tài khoản không tồn tại";
                    }
                    if (check == 2)
                    {
                        ViewBag.Message = "Mật khẩu không chính xác";
                    }
                    if (check >= 3)
                    {
                        var account = _accountBusiness.GetAccountByUsername(loginModel.Username);
                        loginModel.TypeUser = account.AccountType;
                        loginModel.ID = account.ID;
                        HttpContext.Session.Set<LoginModel>("LoginModel", loginModel);
                        if (loginModel.TypeUser == 1)
                        {
                            return Redirect("/Admin/Home");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    switch (check)
                    {
                        case 1:
                            ViewBag.Message = "Tài khoản không tồn tại";
                            break;
                        case 2:
                            ViewBag.Message = "Mật khẩu không chính xác";
                            break;
                    }
                }
            }
            return View(loginModel);

        }
    }
}