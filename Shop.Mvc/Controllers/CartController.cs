using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using Shop.Mvc.Entensions;
using Shop.Mvc.Models;
using System.Drawing;
using System.Linq.Expressions;

namespace Shop.Mvc.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductBusiness _productBusiness;
        private readonly IFileBusiness _fileBusiness;
        public CartController(IProductBusiness productBusiness, IFileBusiness fileBusiness)
        {
            
            _productBusiness = productBusiness;
            _fileBusiness = fileBusiness;
        }
        [HttpGet]
        [Route("/Cart")]
        public IActionResult Index()
        {
            try
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
                ViewData["ListFile"] = listFile;
                ViewData["ListCart"] = listCart;
                ViewBag.Total = total;
                return View();
            }
            catch(Exception ex)
            {
                return Redirect("/404");
            }
            
        }
        [HttpPost]
        public IActionResult DeleteItem(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var listCart = HttpContext.Session.Get<List<CartItem>>("ListCart");
                listCart.Remove(listCart.SingleOrDefault(item => item.Product.ID == long.Parse(id)));
                long total = 0;
                foreach (var item in listCart)
                {
                    total += item.TotalMoney;
                }
                HttpContext.Session.Set<List<CartItem>>("ListCart", listCart);
                return Json(new
                {
                    status = true,
                    total = total
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
        [HttpPost]
        public IActionResult Insert(string id,string color="1",string size="2",string amount="1")
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                {
                    return Json(new
                    {
                        status = true,
                        isLogin = false,
                    });
                }
                var listCart = HttpContext.Session.Get<List<CartItem>>("ListCart");
                if (listCart != null)
                {
                    foreach (var item in listCart)
                    {
                        if (long.Parse(id) == item.Product.ID)
                        {
                            return Json(new
                            {
                                status = true,
                                isInsert = false
                            });
                        }
                    }
                }
                else
                {
                    listCart = new List<CartItem>();
                }
                var cart = new CartItem();
                var productDTO = _productBusiness.GetProductByID(long.Parse(id));
                cart.Product = productDTO;
                cart.Size = size;
                cart.Color = color;
                cart.Amount = int.Parse(amount);
                listCart.Add(cart);
                HttpContext.Session.Set<List<CartItem>>("ListCart", listCart);
                return Json(new
                {
                    status = true,
                    isInsert = true
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
        [HttpPost]
        public IActionResult ChangeSize(string id,string size)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var listCart = HttpContext.Session.Get<List<CartItem>>("ListCart");
                foreach (var item in listCart.Where(x => x.Product.ID == long.Parse(id)))
                {
                    item.Size = size;
                }
                HttpContext.Session.Set<List<CartItem>>("ListCart", listCart);
                return Json(new
                {
                    status = true
                });
            }catch(Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
        [HttpPost]
        public IActionResult ChangeAmount(string id,string amount)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var listCart = HttpContext.Session.Get<List<CartItem>>("ListCart");
                long total = 0;
                long _totalMoney = 0;
                foreach (var item in listCart)
                {
                    if (item.Product.ID == long.Parse(id))
                    {
                        item.Amount = int.Parse(amount);
                        total = item.TotalMoney;
                    }
                    _totalMoney += item.TotalMoney;
                }
                HttpContext.Session.Set<List<CartItem>>("ListCart", listCart);
                return Json(new
                {
                    status = true,
                    total = total,
                    totalMoney = _totalMoney
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
        [HttpPost]
        public IActionResult ChangeColor(string id,string color)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
            try
            {
                var listCart = HttpContext.Session.Get<List<CartItem>>("ListCart");
                foreach (var item in listCart.Where(x => x.Product.ID == long.Parse(id)))
                {
                    item.Color = color;
                }
                HttpContext.Session.Set<List<CartItem>>("ListCart", listCart);
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}
