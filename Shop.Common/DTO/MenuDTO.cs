using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.DTO
{
    public class MenuDTO
    {
        public long ID { set; get; }
        public string Name { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public long CreatedBy { set; get; }
        public bool Status { set; get; }
    }
}
