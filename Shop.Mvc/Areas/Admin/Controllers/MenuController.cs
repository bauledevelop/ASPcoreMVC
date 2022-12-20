using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.Implements;
using Shop.Business.Interfaces;
using Shop.Mvc.Areas.Admin.Mapper;
using Shop.Mvc.Areas.Admin.Models;
using Shop.Mvc.Commons.Models;
using Shop.Mvc.Entensions;
using Shop.Mvc.Models;
using X.PagedList;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MenuController : Controller
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
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            try
            {
                var model = _menuBusiness.SelectByQuantityItem(page, pageSize);
                var menuViewModels = new List<MenuViewModel>();
                var mapperMenu = new MenuMapper();
                var mapperAccount = new AccountMapper();
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
                    ViewData["ListAccount"] = accountViewModel;
                    ViewData["Pagination"] = _menuBusiness.SelectAll().ToPagedList(page,pageSize);
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
        public async Task<IActionResult> Delete(string id)
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
        public async Task<IActionResult> Create(MenuViewModel menuViewModel)
        {
            if (!ModelState.IsValid) return View(menuViewModel);
            try
            {
                var mapperMenu = new MenuMapper();
                var menuDTO = mapperMenu.MapperViewModelToDto(menuViewModel);
                var accountDTO = _accountBusiness.GetAccountByUsername(User.Identity.Name);
                menuDTO.CreatedBy = accountDTO.ID;

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
        public async Task<IActionResult> Edit(MenuViewModel menuViewModel)
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
