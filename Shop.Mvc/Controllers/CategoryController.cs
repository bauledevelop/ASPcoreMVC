using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using Shop.Mvc.Commons.Models;

namespace Shop.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductBusiness _productBusiness;
        private readonly IMenuBusiness _menuBusiness;
        private readonly ICategoryProductBusiness _categoryProductBusiness;
        private readonly IFileBusiness _fileBusiness;
        public CategoryController(IProductBusiness productBusiness, IMenuBusiness menuBusiness,ICategoryProductBusiness categoryProductBusiness,IFileBusiness fileBusiness)
        {
            _productBusiness = productBusiness;
            _menuBusiness = menuBusiness;
            _categoryProductBusiness = categoryProductBusiness;
            _fileBusiness = fileBusiness;
        }
        [Route("Category/{id?}")]
        public IActionResult Index(string id,int page=1,int pageSize=8)
        {
            try
            {
                var productDTOs = _productBusiness.SelectByIDCategoryQuantityItem(page, pageSize, long.Parse(id));
                var total = _productBusiness.GetTotalByIDCategory(long.Parse(id));
                var menuDTOs = _menuBusiness.SelectAllByStatus();
                var listFile = _fileBusiness.SelectAll();
                var categoryDTOs = _categoryProductBusiness.SelectAllCategoryByStatus();
                var pagination = new PaginationModel();
                pagination.Total = total;
                pagination.Show = (total != 0 ? ((page - 1) * pageSize) + 1 : 0);
                pagination.ShowTo = (((page - 1) * pageSize) + 1) + productDTOs.Count() - 1;
                pagination.Page = page;
                int maxPage = 5;
                int totalPage = 0;
                totalPage = (int)Math.Ceiling((double)((double)total / (double)pageSize));
                pagination.TotalPage = totalPage;
                pagination.MaxPage = 5;
                pagination.First = 1;
                pagination.Last = totalPage;
                pagination.Next = page + 1;
                pagination.Prev = page - 1;
                foreach(var item in categoryDTOs)
                {
                    if (item.ID == long.Parse(id))
                    {
                        ViewBag.TitleCategory = item.Name;
                        foreach(var child in menuDTOs)
                        {
                            if (item.IDMenu == child.ID)
                            {
                                ViewBag.TitleMenu = child.Name;
                                break;
                            }
                        }
                        
                    }
                }
                ViewData["ListProduct"] = productDTOs;
                ViewData["ListMenu"] = menuDTOs;
                ViewData["ListCategory"] = categoryDTOs;
                ViewData["ListFile"] = listFile;
                ViewData["Pagination"] = pagination;
                return View();
            }
            catch(Exception ex)
            {
                return Redirect("/404");
            }
        }
    }
}
