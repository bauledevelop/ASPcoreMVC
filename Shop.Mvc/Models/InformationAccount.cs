using System.ComponentModel.DataAnnotations;

namespace Shop.Mvc.Models
{
    public class InformationAccount
    {
        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Vui lòng điền tên ")]
        public string Name { set; get; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Vui lòng điền ngày sinh")]
        public DateTime BirthDay { set; get; }
        public string Address { set; get; }
        [Display(Name = "Địa chỉ Email")]
        public string? Email { set; get; }
        [Display(Name = "Giới tính")]
        public string Sex { set; get; }
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng điền số điện thoại")]
        public string Phone { set; get; }
    }
}
