using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.DTO
{
    public class FeedbackDTO
    {
        public long ID { set; get; }
        public string Name { set; get; }
        public string Content { set; get; }
        public DateTime CreatedDate { set; get; }
        public bool Status { set; get; }
        public long IDAcount { set; get; }
    }
}
