using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.MailHelper
{
    public class EmailConfiguration
    {
        public string To { set; get; }
        public string Subject { set; get; }
        public string Body { set; get; }
    }
}
