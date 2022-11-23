using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;

namespace Shop.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductBusiness _productBusiness;
        private readonly IFileBusiness _fileBusiness;
        public ProductController(IProductBusiness productBusiness,IFileBusiness fileBusiness)
        {
            _productBusiness = productBusiness;
            _fileBusiness = fileBusiness;
        }
        [Route("Product/{id?}")]
        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var model = _productBusiness.SelectById(long.Parse(id));
                var relatedProducts = _productBusiness.SelectRelatedProduct(model.IDCategoryProduct,long.Parse(id));
                var listFile = _fileBusiness.SelectAll();
                ViewData["Product"] = model;
                ViewData["ListProduct"] = relatedProducts;
                ViewData["ListFile"] = listFile;
                return View();
            }
            catch(Exception ex)
            {
                return Redirect("/404");
            }
        }
    }
}
