using Microsoft.AspNetCore.Mvc;
using Shop.Business.Implements;
using Shop.Business.Interfaces;
using Shop.Mvc.Areas.Admin.Mapper;
using Shop.Mvc.Areas.Admin.Models;
using Shop.Mvc.Commons.Models;
using Shop.Mvc.Entensions;
using Shop.Mvc.Models;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuBusiness _menuBusiness;
        private readonly IAccountBusiness _accountBusiness;
        public MenuController(IMenuBusiness menuBusiness,IAccountBusiness accountBusiness)
        {
            _menuBusiness = menuBusiness;
            _accountBusiness = accountBusiness;
        }

        [Area("Admin")]
        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 1)
        {
            try
            {
                var model = _menuBusiness.SelectByQuantityItem(page, pageSize);
                var menuViewModels = new List<MenuViewModel>();
                var mapperMenu = new MenuMapper();
                var mapperAccount = new AccountMapper();
                var pagination = new PaginationModel();
                var total = _menuBusiness.GetTotal();
                var listAccount = _accountBusiness.SelectAll();
                var accountViewModel = new List<AccountViewModel>();
                if (model != null)
                {
                    foreach (var item in listAccount)
                    {
                        accountViewModel.Add(mapperAccount.MapperDtoToViewModel(item));
                    }
                    foreach (var item in model)
                    {
                        menuViewModels.Add(mapperMenu.MapperDtoToViewModel(item));
                    }
                    pagination.Total = total;
                    pagination.Show = (total != 0 ? ((page - 1) * pageSize) + 1 : 0);
                    pagination.ShowTo = (((page - 1) * pageSize) + 1) + model.Count() - 1;
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
                    ViewData["ListAccount"] = accountViewModel;
                    ViewData["Pagination"] = pagination;
                }
                return View(menuViewModels);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                _menuBusiness.DeleteMenu(long.Parse(id));
                return Json(new
                {
                    status = true
                });
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Create(MenuViewModel menuViewModel)
        {
            if (!ModelState.IsValid) return View(menuViewModel);
            try
            {
                var mapperMenu = new MenuMapper();
                var menuDTO = mapperMenu.MapperViewModelToDto(menuViewModel);
                menuDTO.CreatedBy = HttpContext.Session.Get<LoginModel>("LoginModel").ID;
                _menuBusiness.InsertMenu(menuDTO);
                return Redirect("/Admin/Menu");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Thêm Menu không thành công";
            }
            return View(menuViewModel);
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var mapperMenu = new MenuMapper();
                var menuDTO = _menuBusiness.GetMenuById(long.Parse(id));
                var model = mapperMenu.MapperDtoToViewModel(menuDTO);
                return View(model);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Edit(MenuViewModel menuViewModel)
        {
            if (!ModelState.IsValid) return View(menuViewModel);
            try
            {
                var mapperMenu = new MenuMapper();
                var menuDTO = mapperMenu.MapperViewModelToDto(menuViewModel);
                _menuBusiness.EditMenu(menuDTO);
                return Redirect("/Admin/Menu");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Chỉnh sửa loại sản phẩm thất bại";
            }
            return View(menuViewModel);
        }
        [Area("Admin")]
        [HttpPost]
        public JsonResult GetData()
        {
            try
            {
                var mapperMenu = new MenuMapper();
                var menuDTOs = _menuBusiness.SelectAll();
                var menuViewModel = menuDTOs.Select(item => mapperMenu.MapperDtoToViewModel(item));
                return Json(new
                {
                    status = true,
                    data = menuViewModel
                });
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}
