using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Models
{
    public class FileViewModel
    {
        public long ID { set; get; }
        [Display(Name = "Đường dẫn")]
        public string? FileContent { set; get; }
        [Display(Name = "Loại dữ liệu")]
        [Required(ErrorMessage = "Vui lòng chọn loại dữ liệu")]
        public string Type { set; get; }
        [Display(Name = "Loại sản phẩm")]
        public string IDCategory { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public long CreatedBy { set; get; }
        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
        [Display(Name = "Sản phẩm")]
        [Required(ErrorMessage = "Vui lòng chọn sản phẩm")]
        public string IDProduct { set; get; }
    }
}
