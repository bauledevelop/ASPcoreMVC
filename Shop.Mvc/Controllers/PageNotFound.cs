using Microsoft.AspNetCore.Mvc;

namespace Shop.Mvc.Controllers
{
    public class PageNotFound : Controller
    {
        [Route("404")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
