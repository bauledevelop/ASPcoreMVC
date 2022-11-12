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
        public string FileContent { set; get; }
        [Display(Name = "Loại dữ liệu")]
        public int Type { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public long? CreatedBy { set; get; }
        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
        public long? IDProduct { set; get; }
    }
}
