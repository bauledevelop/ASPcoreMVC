using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using X.PagedList;

namespace Shop.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductBusiness _productBusiness;
        private readonly IMenuBusiness _menuBusiness;
        private readonly ICategoryProductBusiness _categoryProductBusiness;
        private readonly IFileBusiness _fileBusiness;
        public CategoryController(IProductBusiness productBusiness, IMenuBusiness menuBusiness, ICategoryProductBusiness categoryProductBusiness, IFileBusiness fileBusiness)
        {
            _productBusiness = productBusiness;
            _menuBusiness = menuBusiness;
            _categoryProductBusiness = categoryProductBusiness;
            _fileBusiness = fileBusiness;
        }
        [Route("Category/{id?}")]
        public IActionResult Index(string id, int page = 1, int pageSize = 8)
        {
            try
            {
                var productDTOs = _productBusiness.SelectByIDCategoryQuantityItem(page, pageSize, long.Parse(id));
                var menuDTOs = _menuBusiness.SelectAllByStatus();
                var listFile = _fileBusiness.SelectAll();
                var categoryDTOs = _categoryProductBusiness.SelectAllCategoryByStatus();
                var listTrend = _productBusiness.SelectTrendProduct();
                foreach (var item in categoryDTOs)
                {
                    if (item.ID == long.Parse(id))
                    {
                        ViewBag.TitleCategory = item.Name;
                        foreach (var child in menuDTOs)
                        {
                            if (item.IDMenu == child.ID)
                            {
                                ViewBag.TitleMenu = child.Name;
                                break;
                            }
                        }

                    }
                }
                ViewBag.ID = id;
                ViewData["Pagination"] = _productBusiness.SelectByIDCategory(long.Parse(id)).ToPagedList(page,pageSize);
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
