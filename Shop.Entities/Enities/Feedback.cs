using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities.Enities
{
    public class Feedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }
        [Column(TypeName ="nvarchar")]
        [StringLength(1024)]
        public string Name { set; get; }
        [Column(TypeName = "nvarchar")]
        [StringLength(2048)]
        public string Content { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { set; get; }
        public bool IsDelete { set; get; }
        public bool Status { set; get; }
        public long IDAcount { set; get; }
        [ForeignKey("IDAcount")]
        public virtual Account Account { set; get; }
    }
}
