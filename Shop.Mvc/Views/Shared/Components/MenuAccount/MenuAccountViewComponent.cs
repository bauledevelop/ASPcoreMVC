using Microsoft.AspNetCore.Mvc;

namespace Shop.Mvc.Views.Shared.Components.MenuAccount
{
    public class MenuAccountViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
