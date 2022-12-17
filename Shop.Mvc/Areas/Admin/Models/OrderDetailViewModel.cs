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
        [Display(Name = "Nội dung")]
        public string Name { set; get; }
        [Display(Name = "Số lượng")]
        public long Quantity { set; get; }
        [Display(Name = "Tổng tiền")]
        public long Total { set; get; }
        public string Size { set; get; }
        public string Color { set; get; }
        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
    }
}
