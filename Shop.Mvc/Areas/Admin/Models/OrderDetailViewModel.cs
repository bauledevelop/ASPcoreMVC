using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Models
{
    public class OrderDetailViewModel
    {
        public long ID { set; get; }
        [Display(Name = "Số lượng")]
        public long Quantity { set; get; }
        [Display(Name = "Tổng tiền")]
        public long Total { set; get; }
        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
        public long IDOrder { set; get; }
        public long IDProduct { set; get; }
    }
}
