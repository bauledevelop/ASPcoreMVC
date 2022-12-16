using Microsoft.AspNetCore.Mvc;

namespace Shop.Mvc.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
