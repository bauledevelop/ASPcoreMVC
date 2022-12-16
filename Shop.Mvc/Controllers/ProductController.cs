using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using Shop.Mvc.Entensions;
using Shop.Mvc.Models;

namespace Shop.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductBusiness _productBusiness;
        private readonly IFileBusiness _fileBusiness;
        private readonly IOrderDetailBusiness _orderDetailBusiness;
        private readonly IRateBusiness _rateBusiness;
        private readonly ICommentBusiness _commentBusiness;
        private readonly ICategoryProductBusiness _categoryProductBusiness;
        private readonly IMenuBusiness _menuBusiness;
        private readonly IAccountBusiness _accountBusiness;
        public ProductController(IProductBusiness productBusiness, IFileBusiness fileBusiness, IOrderDetailBusiness orderDetailBusiness
            , IRateBusiness rateBusiness, ICommentBusiness commentBusiness, ICategoryProductBusiness categoryProductBusiness
            , IMenuBusiness menuBusiness, IAccountBusiness accountBusiness)
        {
            _productBusiness = productBusiness;
            _fileBusiness = fileBusiness;
            _orderDetailBusiness = orderDetailBusiness;
            _rateBusiness = rateBusiness;
            _commentBusiness = commentBusiness;
            _categoryProductBusiness = categoryProductBusiness;
            _menuBusiness = menuBusiness;
            _accountBusiness = accountBusiness;
        }
        [Route("Product/{id?}")]
        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var model = _productBusiness.SelectById(long.Parse(id));
                var loginModel = _accountBusiness.GetAccountByUsername(User.Identity.Name);
                var relatedProducts = _productBusiness.SelectRelatedProduct(model.IDCategoryProduct, long.Parse(id));
                var listFile = _fileBusiness.SelectAll();
                var rateProduct = _rateBusiness.SelectByIDProduct(long.Parse(id));
                var rateDTO = (loginModel != null) ? _rateBusiness.GetRateByIDAccount(loginModel.ID).SingleOrDefault(x => x.IDProduct == long.Parse(id)) : null;
                var totalComment = _commentBusiness.GetTotalByIDProduct(long.Parse(id));
                var category = _categoryProductBusiness.GetCategoryById(model.IDCategoryProduct);
                var menu = _menuBusiness.GetMenuById(category.IDMenu);
                var account = _accountBusiness.GetAccountById(model.CreatedBy);
                if (loginModel != null)
                {
                    var commentDTO = _commentBusiness.GetCommentByIDAccount(loginModel.ID,long.Parse(id));
                    ViewData["Comment"] = commentDTO;
                }
                else
                {
                    ViewData["Comment"] = null;
                }
                double amount = rateProduct.Count();
                double tbRate = 0;
                foreach (var item in rateProduct)
                {
                    tbRate += item.rate;
                }
                tbRate = Math.Round(tbRate / amount);
                ViewBag.Amount = amount;
                ViewBag.TotalRate = tbRate;
                if (ViewBag.Amount == 0)
                    ViewBag.Rate = 0;
                ViewBag.AmountSell = _orderDetailBusiness.GetAmountByIDProduct(long.Parse(id));
                ViewData["Product"] = model;
                ViewData["Rate"] = rateDTO;
                ViewData["ListFile"] = listFile;
                ViewData["ListProduct"] = relatedProducts;
                ViewData["Category"] = category;
                ViewBag.TotalComment = totalComment;
                ViewBag.Menu = menu.Name;
                ViewBag.Account = account.Username;
                return View();
            }
            catch (Exception ex)
            {
                return Redirect("/404");
            }
        }
    }
}
