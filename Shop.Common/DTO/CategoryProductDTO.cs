using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.DTO
{
    public class CategoryProductDTO
    {
        public long ID { set; get; }
        public string Name { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdateDate { set; get; }
        public long IDMenu { set; get; }
        public bool Status { set; get; }
        public long CreatedBy { set; get; }
    }
}
