using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public long ID { set; get; }
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Vui lòng điền tên sản phẩm")]
        public string Name { set; get; }
        [Display(Name = "Mô tả sản phẩm")]
        [Required(ErrorMessage = "Vui lòng điền mô tả")]
        public string Description { set; get; }
        [Display(Name = "Chi tiết sản phẩm")]
        [Required(ErrorMessage = "Vui lòng điền chi tiết sản phẩm")]
        public string Detail { set; get; }
        [Display(Name = "Giá tiền")]
        [Required(ErrorMessage = "Vui lòng điền giá tiền")]
        public string Price { set; get; }
        
        [Display(Name = "Menu")]
        public string IDMenu { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
        [Display(Name = "Loại sản phẩm")]
        [Required(ErrorMessage = "Vui lòng chọn loại sản phẩm")]
        public string IDCategoryProduct { set; get; }
        public long CreatedBy { set; get; }
    }
}
