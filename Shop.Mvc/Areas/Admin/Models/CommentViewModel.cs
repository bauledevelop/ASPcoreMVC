using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Models
{
    public class CommentViewModel
    {
        public long ID { set; get; }
        [Display(Name = "Nội dung")]
        public string Content { set; get; }
        public DateTime CreatedDate { set; get; }
        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
        public long? IDAccount { set; get; }
        public long? IDProduct { set; get; }
    }
}
