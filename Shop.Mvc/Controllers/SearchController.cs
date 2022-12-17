using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using X.PagedList;

namespace Shop.Mvc.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductBusiness _productBusiness;
        private readonly IMenuBusiness _menuBusiness;
        private readonly ICategoryProductBusiness _categoryProductBusiness;
        private readonly IFileBusiness _fileBusiness;
        public SearchController(IProductBusiness productBusiness, IMenuBusiness menuBusiness, ICategoryProductBusiness categoryProductBusiness, IFileBusiness fileBusiness) 
        {
            _productBusiness = productBusiness;
            _menuBusiness = menuBusiness;
            _categoryProductBusiness = categoryProductBusiness;
            _fileBusiness = fileBusiness;
        }
        [HttpGet]
        public IActionResult Index(string search, int page=1,int pageSize=8)
        {
            if (string.IsNullOrEmpty(search)) throw new ArgumentNullException(nameof(search));
            try
            {
                var productDTOs = _productBusiness.SelectByKeyWordQuantityItem(page, pageSize, search);
                var menuDTOs = _menuBusiness.SelectAllByStatus();
                var listFile = _fileBusiness.SelectAll();
                var categoryDTOs = _categoryProductBusiness.SelectAllCategoryByStatus();
                var listTrend = _productBusiness.SelectTrendProduct();
                ViewBag.Search = search;
                ViewData["Pagination"] = (productDTOs.Count() == 0) ? null : _productBusiness.SelectByKeyWord(search).ToPagedList(page, pageSize);
                ViewData["ListTrend"] = listTrend;
                ViewData["ListProduct"] = productDTOs;
                ViewData["ListMenu"] = menuDTOs;
                ViewData["ListCategory"] = categoryDTOs;
                ViewData["ListFile"] = listFile;

                return View();
            }
            catch (Exception ex)
            {
                return Redirect("/404");
            }
        }
    }
}
