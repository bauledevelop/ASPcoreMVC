using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Mvc.Entensions;
using Shop.Mvc.Models;
using PayPal.Core;
using PayPal.v1.Payments;
using BraintreeHttp;
using Shop.Business.Implements;

namespace Shop.Mvc.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IAccountBusiness _accountBusiness;
        private readonly IOrderBusiness _orderBusiness;
        private readonly IOrderDetailBusiness _orderDetailBusiness;
        private readonly IEmailSender _emailSender;
        private readonly string _clientId;
        private readonly string _secretKey;
        public double TyGia = 1000;
        public CheckoutController(IAccountBusiness accountBusiness, IOrderBusiness orderBusiness,
            IOrderDetailBusiness orderDetailBusiness, IConfiguration config,IEmailSender emailSender)
        {
            _accountBusiness = accountBusiness;
            _orderBusiness = orderBusiness;
            _orderDetailBusiness = orderDetailBusiness;
            _emailSender = emailSender;
            _clientId = config["PaypalSettings:ClientId"];
            _secretKey = config["PaypalSettings:SecretKey"];
        }
        [Authorize,HttpGet]
        public IActionResult Index()
        {
            try
            {
                var accountDTO = _accountBusiness.GetAccountByUsername(User.Identity.Name);
                var _checkout = new CheckoutViewModel();
                var listCart = HttpContext.Session.Get<List<CartItem>>("ListCart");
                long total = 0;
                foreach(var item in listCart)
                {
                    total += item.TotalMoney;
                }
                _checkout.Email = accountDTO.Email;
                _checkout.Name = accountDTO.Name;
                _checkout.BirthDay = accountDTO.BirthDay;
                _checkout.Address = accountDTO.Address;
                _checkout.Phone = accountDTO.Phone;
                _checkout.Sex = ((accountDTO.Sex == 1) ? "Nam" : "Nữ");
                ViewBag.Total = total;
                ViewData["ListCart"] = listCart;
                return View(_checkout);
            }
            catch(Exception ex)
            {
                return Redirect("/404");
            }
        }
        [Authorize,HttpGet]
        public async Task<IActionResult> PaypalCheckout()
        {
            var enviroment = new SandboxEnvironment(_clientId, _secretKey);
            var client = new PayPalHttpClient(enviroment);

            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };
            var Carts = HttpContext.Session.Get<List<CartItem>>("ListCart");
            var total = Math.Round(Carts.Sum(p => p.TotalMoney)/TyGia);
            foreach(var item in Carts)
            {
                itemList.Items.Add(new Item()
                {
                    Name = item.Product.Name,
                    Currency = "USD",
                    Price = Math.Round(item.Product.Price / TyGia).ToString(),
                    Quantity = item.Amount.ToString(),
                    Sku = "sku",
                    Tax = "0"
                });
            }
            var paypalOrderId = DateTime.Now.Ticks;
            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>() {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = total.ToString(),
                            Currency = "USD",
                            Details = new AmountDetails
                            {
                                Tax="0",
                                Shipping = "0",
                                Subtotal = total.ToString()
                            }
                        },
                        ItemList = itemList,
                        Description = $"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId.ToString()
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = $"{hostname}/Checkout/CheckoutFail",
                    ReturnUrl = $"{hostname}/Checkout/CheckoutSuccess"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };
            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);
            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while(links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if(lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        paypalRedirectUrl = lnk.Href;
                    }
                }
                return Redirect(paypalRedirectUrl);
            }
            catch(HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").SingleOrDefault();

                return Redirect("/Checkout/CheckoutFail");
            }
        }
        public async Task<IActionResult> CheckoutFail()
        {
            return View();
        }

        public async Task<IActionResult> CheckoutSuccess()
        {
            try
            {
                var listCart = HttpContext.Session.Get<List<CartItem>>("ListCart");
                var loginModel = _accountBusiness.GetAccountByUsername(User.Identity.Name);
                long total = 0;
                long _quantity = 0;
                foreach (var item in listCart)
                {
                    total += item.TotalMoney;
                    _quantity += item.Amount;
                }
                var orderDTO = new OrderDTO();
                orderDTO.Total = total;
                orderDTO.Quantity = _quantity;
                orderDTO.IDAccount = loginModel.ID;
                var idOrder = _orderBusiness.InsertOrder(orderDTO);
                var orderDetaiDTO = new OrderDetailDTO();
                foreach (var item in listCart)
                {
                    orderDetaiDTO.IDOrder = idOrder;
                    orderDetaiDTO.IDProduct = item.Product.ID;
                    orderDetaiDTO.Quantity = item.Amount;
                    orderDetaiDTO.Color = int.Parse(item.Color);
                    orderDetaiDTO.Total = item.TotalMoney;
                    orderDetaiDTO.Size = int.Parse(item.Size);
                    _orderDetailBusiness.InsertOrderDetail(orderDetaiDTO);
                }
                var mailContent = new MailContent();
                mailContent.To = loginModel.Email;
                mailContent.Subject = "Xác nhận đơn hàng";
                string content = string.Empty;
                using (StreamReader reader = new StreamReader(Path.Combine("assets/template/NewOrder.html")))
                {
                    content = reader.ReadToEnd();
                }
                content = content.Replace("{{CustomerName}}", loginModel.Name);
                content = content.Replace("{{Phone}}", loginModel.Phone);
                content = content.Replace("{{Email}}", loginModel.Email);
                content = content.Replace("{{Address}}", loginModel.Address);
                content = content.Replace("{{Total}}", orderDTO.Total.ToString("N0"));
                mailContent.Body = content;
                await _emailSender.SendMail(mailContent);
                listCart = null;
                HttpContext.Session.Set<List<CartItem>>("ListCart", listCart);
                return View();
            }
            catch (Exception ex)
            {
                return Redirect("/404");
            }
        }

    }
}
