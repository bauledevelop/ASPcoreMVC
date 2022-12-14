using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using Shop.Mvc.Entensions;
using Shop.Mvc.Models;

namespace Shop.Mvc.Views.Shared.Components.Footer
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly IProductBusiness _productBusiness;
        private readonly IFileBusiness _fileBusiness;
        public FooterViewComponent(IProductBusiness productBusiness, IFileBusiness fileBusiness)
        {
            _productBusiness = productBusiness;
            _fileBusiness = fileBusiness;
        }
        public IViewComponentResult Invoke()
        {
            var listCart = HttpContext.Session.Get<List<CartItem>>("ListCart");
            var listFile = _fileBusiness.SelectAll();
            long total = 0;
            if (listCart != null)
            {
                foreach (var item in listCart)
                {
                    total += item.TotalMoney;
                }
            }
            ViewBag.Total = total;
            ViewData["ListCart"] = listCart;
            ViewData["ListFile"] = listFile;
            return View();
        }
    }
}
