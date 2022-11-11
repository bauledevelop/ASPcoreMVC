using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities.Enities
{
    public class OrderDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }
        public long Quantity { set; get; }
        public long Total { set; get; }
        public bool Status { set; get; }
        public long? IDOrder { set; get; }
        public long? IDProduct { set; get; }
        [ForeignKey("IDOrder")]
        public virtual Order Order { set; get; }
        [ForeignKey("IDProduct")]
        public virtual Product Product { set; get; }
    }
}
