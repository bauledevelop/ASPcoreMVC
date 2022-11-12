using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Models
{
    public class OrderViewModel
    {
        public long ID { set; get; }
        [Display(Name ="Số lượng")]
        public long Quantity { set; get; }
        public DateTime CreatedDate { set; get; }
        [Display(Name = "Tổng tiền")]
        public long Total { set; get; }
        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
        public long? IDAccount { set; get; }
    }
}
