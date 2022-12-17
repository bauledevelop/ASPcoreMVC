using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities.Enities
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }
        [Column(TypeName = "nvarchar")]
        [StringLength(2048)]
        public string Content { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { set; get; }
        public bool IsDelete { set; get; }
        public bool Status { set; get; }
        public long IDAccount { set; get; }
        public long IDProduct { set; get; }
        [ForeignKey("IDAccount")]
        public virtual Account Account { set; get; }
        [ForeignKey("IDProduct")]
        public virtual Product Product { set; get; }
    }
}
