using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.DTO
{
    public class FileDTO
    {
        public long ID { set; get; }
        public string FileContent { set; get; }
        public int Type { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public bool Status { set; get; }
        public long CreatedBy { set; get; }
        public long IDProduct { set; get; }
    }
}
