using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.DTO
{
    public class OrderDTO
    {
        public long ID { set; get; }
        public long Quantity { set; get; }
        public DateTime CreatedDate { set; get; }
        public long Total { set; get; }
        public bool Status { set; get; }
        public long IDAccount { set; get; }
    }
}
