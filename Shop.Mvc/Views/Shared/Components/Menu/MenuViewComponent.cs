using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;

namespace Shop.Mvc.Views.Shared.Components.Header
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ICategoryProductBusiness _categoryProductBusiness;
        private readonly IMenuBusiness _menuBusiness;
        public MenuViewComponent(ICategoryProductBusiness categoryProductBusiness, IMenuBusiness menuBusiness)
        {
            _categoryProductBusiness = categoryProductBusiness;
            _menuBusiness = menuBusiness;
        }
        public IViewComponentResult Invoke()
        {
            ViewData["ListCategory"] = _categoryProductBusiness.SelectAllCategoryByStatus();
            ViewData["ListMenu"] = _menuBusiness.SelectAllByStatus();
            return View();
        }
    }
}
