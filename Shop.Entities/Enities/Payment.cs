using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities.Enities
{
    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }
        [StringLength(1024)]
        public string Email { set; get; }
        [StringLength(20)]
        public string Phone { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { set; get; }
        public bool IsDelete { set; get; }
        public bool Status { set; get; }
        public long IDAccount { set; get; }
        public long IDOrder { set; get; }
        [ForeignKey("IDAccount")]
        public virtual Account Account { set; get; }
        [ForeignKey("IDOrder")]
        public virtual Order Order { set; get; }
    }
}
