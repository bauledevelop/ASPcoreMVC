using System.ComponentModel.DataAnnotations;

namespace Shop.Mvc.Models
{
    public class LoginModel
    {
        public long ID { set; get; }
        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Vui lòng điền tài khoản")]
        public string Username { set; get; }
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vui lòng điền mật khẩu")]
        public string Password { set; get; }

        public int TypeUser { set; get; }
    }
}
