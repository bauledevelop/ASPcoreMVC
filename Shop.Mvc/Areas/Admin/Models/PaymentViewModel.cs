using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Models
{
    public class PaymentViewModel
    {
        public long ID { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public bool Status { set; get; }
        public long? IDAccount { set; get; }
        public long? IDOrder { set; get; }
    }
}
