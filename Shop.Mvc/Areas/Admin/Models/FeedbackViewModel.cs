using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Models
{
    public class FeedbackViewModel
    {
        public long ID { set; get; }
        [Display(Name = "Tiêu đề phản hồi")]
        public string Name { set; get; }
        [Display(Name = "Nội dung")]
        public string Content { set; get; }
        public DateTime CreatedDate { set; get; }
        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
        public long IDAcount { set; get; }
    }
}
