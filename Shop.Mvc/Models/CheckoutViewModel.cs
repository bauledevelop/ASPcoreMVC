using System.ComponentModel.DataAnnotations;

namespace Shop.Mvc.Models
{
    public class CheckoutViewModel
    {
        [Display(Name = "Họ và tên")]
        public string Name { set; get; }
        [Display(Name = "Địa chỉ")]
        public string Address { set; get; }
        [Display(Name = "Ngày sinh")]
        public DateTime BirthDay { set; get; }
        [Display(Name = "Giới tính")]
        public string Sex { set; get; }
        [Display(Name = "Số điện thoại")]
        public string Phone { set; get; }
        [Display(Name = "Địa chỉ Email")]
        public string Email { set; get; }

    }
}
