using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Models
{
    public class CategoryProductViewModel
    {
        public long ID { set; get; }
        [Display(Name = "Tên loại sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập tên loại sản phẩm")]
        public string Name { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdateDate { set; get; }
        [Display(Name = "Trang thái")]
        public bool Status { set; get; }
        public string IDMenu { set; get; }
        public long CreatedBy { set; get; }
       
    }
}
