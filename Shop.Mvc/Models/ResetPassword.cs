using System.ComponentModel.DataAnnotations;

namespace Shop.Mvc.Models
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Hai mật khẩu không khớp nhau")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
