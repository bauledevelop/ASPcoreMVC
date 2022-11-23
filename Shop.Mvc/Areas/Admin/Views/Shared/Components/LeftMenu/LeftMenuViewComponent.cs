using Microsoft.AspNetCore.Mvc;

namespace Mvc.Areas.Admin.Views.Shared.Components
{
    public class LeftMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
