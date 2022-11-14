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
        public string Name { set; get; }
        [Display(Name = "Mô tả sản phẩm")]
        public string Description { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
        public string IDCategoryProduct { set; get; }
        public long CreatedBy { set; get; }
    }
}
