using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities.Enities
{
    public class File
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }
        [StringLength(2048)]
        public string FileContent { set; get; }
        public int Type { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime UpdatedDate { set; get; }
        public long CreatedBy { set; get; }
        public bool IsDelete { set; get; }
        [DefaultValue(false)]
        public bool Status { set; get; }
        public long IDProduct { set; get; }
        [ForeignKey("CreatedBy")]
        public virtual Account Account { set; get; }
        [ForeignKey("IDProduct")]
        public virtual Product Product { set; get; }
    }
}
