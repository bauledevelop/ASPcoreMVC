using Microsoft.AspNetCore.Mvc;

namespace Shop.Mvc.Views.Shared.Components.Model
{
    public class ModelViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
