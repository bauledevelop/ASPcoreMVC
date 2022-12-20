using System.ComponentModel.DataAnnotations;

namespace Shop.Mvc.Models
{
    public class ResetPassword
    {
        [Display(Name ="Mật khẩu hiện tại")]
        [DataType(DataType.Password)]
        public string? CurrentPassword { set; get; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("Password", ErrorMessage = "Hai mật khẩu không khớp nhau")]
        public string ConfirmPassword { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}
