
using System.ComponentModel.DataAnnotations;

namespace Shop.Mvc.Areas.Admin.Models
{
    public class AccountViewModel
    {
        public long ID { set; get; }
        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        [MaxLength(20,ErrorMessage = "Vui lòng nhập không quá 20 kí tự")]
        public string Username { set; get; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MaxLength(20, ErrorMessage = "Vui lòng nhập không quá 20 kí tự")]
        [DataType(DataType.Password)]
        public string Password { set; get; }
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string Name { set; get; }
        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Vui lòng điền ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { set; get; }
        [Display(Name = "Giới tính")]
        public string Sex { set; get; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng điền địa chỉ")]
        public string Address { set; get; }
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng điền số điện thoại")]
        [MaxLength(20,ErrorMessage = "Vui lòng điền không quá 20 số")]
        public string Phone { set; get; }
        [Display(Name = "Địa chỉ Email")]
        [Required(ErrorMessage = "Vui lòng điền Email")]
        public string Email { set; get; }
        public DateTime CreatedDate { set; get; }
        [Display(Name = "Loại người dùng")]
        [Required(ErrorMessage = "Vui lòng chọn loại người dùng")]
        public string AccountType { set; get; }
        public bool IsActive { set; get; }
        public bool IsDelete { set; get; }
        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
    }
}
