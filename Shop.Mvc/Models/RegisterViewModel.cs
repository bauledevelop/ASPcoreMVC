using System.ComponentModel.DataAnnotations;

namespace Shop.Mvc.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng điền tài khoản")]
        [Display(Name= "Tài khoản")]
        [MaxLength(20,ErrorMessage = "Vui lòng điền không quá 20 kí tự")]
        public string Username { set; get; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [Display(Name= "Mật khẩu")]
        [MaxLength(20,ErrorMessage = "Vui lòng điền không quá 20 kí tự")]
        public string Password { set; get; }
        [Display(Name="Họ và tên")]
        [Required(ErrorMessage = "Vui lòng điền họ và tên")]
        public string Name { set; get; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng điền địa chỉ")]
        public string Address { set; get; }
        [MaxLength(15, ErrorMessage ="Vui lòng không nhập quá 15 kí tự")]
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng điền số điện thoại")]
        public string Phone { set; get; }
        [Display(Name = "Địa chỉ Email")]
        [Required(ErrorMessage = "Vui lòng điền địa chỉ Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Vui lòng điền ngày sinh")]
        public DateTime BirthDay { set; get; }
        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        
        public string Sex { set; get; }
    }
}
