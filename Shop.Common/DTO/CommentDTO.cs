using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.DTO
{
    public class CommentDTO
    {
        public long ID { set; get; }
        public string Content { set; get; }
        public DateTime CreatedDate { set; get; }
        public bool Status { set; get; }
        public long IDAccount { set; get; }
        public long IDProduct { set; get; }
    }
}
