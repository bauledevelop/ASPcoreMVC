using System.ComponentModel.DataAnnotations;

namespace Shop.Mvc.Models
{
    public class TwoFactor
    {
        [Required(ErrorMessage = "Vui lòng nhập mã xác thực")]
        [Display(Name = "Mã xác thực")]
        public string TwoFactorCode { get; set; }
    }
}
