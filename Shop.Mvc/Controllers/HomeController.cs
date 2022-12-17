using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Mvc.Commons.DropdownList;
using Shop.Mvc.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using System.Net.Mail;
using System.Net;
using Shop.Business.Implements;
using Microsoft.AspNetCore.Identity;
using Shop.Entities.Enities;
using NuGet.Protocol.Plugins;
using Microsoft.Build.Framework;

namespace Shop.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountBusiness _accountBusiness;
        private readonly IProductBusiness _productBusiness;
        private readonly IFileBusiness _fileBusiness;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly IEmailSender _emailSender;
        private readonly ISlideBusiness _slideBusiness;
        public HomeController(ILogger<HomeController> logger, IAccountBusiness accountBusiness, IProductBusiness productBusiness, IFileBusiness fileBusiness
            , UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher, IEmailSender emailSender,
            ISlideBusiness slideBusiness
            )
        {
            _accountBusiness = accountBusiness;
            _productBusiness = productBusiness;
            _fileBusiness = fileBusiness;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _emailSender = emailSender;
            _slideBusiness = slideBusiness;
        }
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (Email == null)
            {
                ViewBag.Message = "Vui lòng điền Email";
                return View();
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(Email);
                if (user == null || user.EmailConfirmed == false)
                {
                    ViewBag.Message = "Email không tồn tại";
                    return View();
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var link = Url.Action("ResetPassword", "Home", new { token, email = user.Email }, Request.Scheme);
                var mailContent = new MailContent();
                mailContent.To = Email;
                mailContent.Subject = "Đặt lại mật khẩu";
                string content = string.Empty;
                using (StreamReader reader = new StreamReader(Path.Combine("assets/template/verification.html")))
                {
                    content = reader.ReadToEnd();
                }
                content = content.Replace("{{token}}", link);
                mailContent.Body = content;
                await _emailSender.SendMail(mailContent);
                ViewBag.Success = "Gửi mã xác nhận thành công. Vui lòng kiểm tra email";
                return View();
            }
            catch (Exception ex)
            {
                return Redirect("/404");
            }

        }
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
                return View(resetPassword);

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                return RedirectToAction("ResetPasswordFail");

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View();
            }
            _accountBusiness.ResetPassword(user.UserName, resetPassword.Password);
            return RedirectToAction("ResetPasswordConfirmation");
        }
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        public IActionResult ResetPasswordFail()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var listNew = _productBusiness.SelectNewProduct();
                var listFile = _fileBusiness.SelectAll();
                var listTrend = _productBusiness.SelectTrendProduct();
                var listOutStanding = _productBusiness.SelectOutStanding();
                ViewData["ListTrend"] = listTrend;
                ViewData["ListNew"] = listNew;
                ViewData["ListFile"] = listFile;
                ViewData["ListOutStanding"] = listOutStanding;
                ViewData["ListSlide"] = _slideBusiness.SelectAll();
                return View();
            }
            catch (Exception ex)
            {
                return Redirect("/404");
            }

        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpGet]
        public async Task LoginGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });
            return Json(claims);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Register()
        {
            var dropdownList = new DropdownListItem();
            ViewData["TypeSex"] = new SelectList(dropdownList.DropdownListTypeSex(), "Value", "Text");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var dropdownList = new DropdownListItem();
            ViewData["TypeSex"] = new SelectList(dropdownList.DropdownListTypeSexActive(registerViewModel.Sex), "Value", "Text");
            if (!ModelState.IsValid) return View();
            try
            {
                var appUser = new AppUser
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Email,
                    PhoneNumber = registerViewModel.Phone,
                    BirthDay = registerViewModel.BirthDay,
                    Name = registerViewModel.Name,
                    Sex = int.Parse(registerViewModel.Sex),
                    Address = registerViewModel.Address,
                    AccountType = 2
                };
                var result = await _userManager.CreateAsync(appUser, registerViewModel.Password);
                if (result.Succeeded)
                {
                    var role = await _userManager.AddToRoleAsync(appUser, "User");
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var confirmationLink = Url.Action("ConfirmEmail", "Home", new { token, email = registerViewModel.Email }, Request.Scheme);
                    var mailContent = new MailContent();
                    mailContent.To = registerViewModel.Email;
                    mailContent.Subject = "Xác nhận xác thực";
                    string content = string.Empty;
                    using (StreamReader reader = new StreamReader(Path.Combine("assets/template/verification.html")))
                    {
                        content = reader.ReadToEnd();
                    }
                    content = content.Replace("{{token}}", confirmationLink);
                    mailContent.Body = content;
                    await _emailSender.SendMail(mailContent);
                    var accountDTO = new AccountDTO();
                    accountDTO.Username = registerViewModel.Username;
                    accountDTO.Password = registerViewModel.Password;
                    accountDTO.Email = registerViewModel.Email;
                    accountDTO.Phone = registerViewModel.Phone;
                    accountDTO.BirthDay = registerViewModel.BirthDay;
                    accountDTO.Name = registerViewModel.Name;
                    accountDTO.Sex = int.Parse(registerViewModel.Sex);
                    accountDTO.Address = registerViewModel.Address;
                    accountDTO.AccountType = 2;
                    _accountBusiness.InsertAccountByUser(accountDTO);
                    ViewBag.Success = "Tài khoản của bạn đã tạo thành công. Bạn cần xác thực email để sử dụng được tài khoản này";
                    return View();
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Đăng kí tài khoản thất bại";

            }
            return View(registerViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                ViewBag.Check = "true";
                _accountBusiness.SetActiveAccount(user.UserName);
                return View();
            }
            ViewBag.Check = "false";
            return View();

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return View(loginModel);
            try
            {
                AppUser user = await _userManager.FindByNameAsync(loginModel.Username);
                if (user != null)
                {
                    if (user.EmailConfirmed == true)
                    {
                        await _signInManager.SignOutAsync();
                        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                        if (result.Succeeded)
                        {
                            return Redirect("/");
                        }
                        else
                        {
                            ViewBag.Message = "Mật khẩu không chính xác";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Tài khoản của bạn chưa xác thực email";
                    }
                }
                else
                {
                    ViewBag.Message = "Tài khoản không tồn tại";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Đăng nhập tài khoản thất bại";
            }
            return View(loginModel);
        }

    }
}