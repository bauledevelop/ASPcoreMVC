using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.DTO
{
    public class ProductDTO
    {
        public long ID { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public string Detail { set; get; }
        public long Price { set; get; }
        public bool Status { set; get; }
        public long IDCategoryProduct { set; get; }
        public long CreatedBy { set; get; }
    }
}
