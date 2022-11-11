using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.DTO
{
    public class PaymentDTO
    {
        public long ID { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public bool Status { set; get; }
        public long? IDAccount { set; get; }
        public long? IDOrder { set; get; }
    }
}
