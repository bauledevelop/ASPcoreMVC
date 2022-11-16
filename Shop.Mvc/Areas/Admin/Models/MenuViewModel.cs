using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Models
{
    public class MenuViewModel
    {
        public long ID { set; get; }
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Vui lòng nhập tên menu")]
        public string Name { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public long CreatedBy { set; get; }
        [Display(Name = "Trạng thái")]
       
        public bool Status { set; get; }
    }
}
