using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using Shop.Mvc.Entensions;
using Shop.Mvc.Models;

namespace Shop.Mvc.Views.Shared.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IMenuBusiness _menuBusiness;
        private readonly ICategoryProductBusiness _categoryProductBusiness;
        public HeaderViewComponent(IMenuBusiness menuBusiness, ICategoryProductBusiness categoryProductBusiness)
        {
            _menuBusiness = menuBusiness;
            _categoryProductBusiness = categoryProductBusiness;
        }

        public IViewComponentResult Invoke()
        {
            ViewData["ListMenu"] = _menuBusiness.SelectAllByStatus();
            ViewData["ListCategory"] = _categoryProductBusiness.SelectAllCategoryByStatus();
            var listCart = HttpContext.Session.Get<List<CartItem>>("ListCart");
            if (listCart != null)
            {
                ViewBag.Total = listCart.Count();
            }
            else
            {
                ViewBag.Total = 0;
            }
            return View();
        }
    }
}
