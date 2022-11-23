using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Mvc.Entensions;
using Shop.Mvc.Models;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var loginModel = HttpContext.Session.Get<LoginModel>("LoginModel");
            if (loginModel == null || loginModel.TypeUser != 1)
            {
                context.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "PageNotFound", action = "index" }));
            }
            base.OnActionExecuting(context);
        }
    }
}
