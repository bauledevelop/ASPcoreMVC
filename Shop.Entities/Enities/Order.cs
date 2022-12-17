using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities.Enities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }
        public long Quantity { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { set; get; }
        public long Total { set; get; }
        public bool IsDelete { set; get; }
        public bool Status { set; get; }
        public long IDAccount { set; get; }
        [ForeignKey("IDAccount")]
        public virtual Account Account { set; get; }
        public virtual ICollection<Payment> Payments { set; get; }
        public virtual ICollection<OrderDetail> OrderDetails { set; get; }
    }
}
