using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.DTO
{
    public class RateDTO
    {
        public long ID { set; get; }
        public int rate { set; get; }
        public long IDProduct { set; get; }
        public long IDAccount { set; get; }
    }
}
