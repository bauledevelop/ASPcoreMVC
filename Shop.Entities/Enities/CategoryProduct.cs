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
    public class CategoryProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }
        [Column(TypeName = "nvarchar")]
        [StringLength(1024)]
        public string Name { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime UpdateDate { set; get; }
        [DefaultValue(false)]
        public bool Status { set; get; }
        public long? CreatedBy { set; get; }
        [ForeignKey("CreatedBy")]
        public virtual Account Account { set; get; }
        public virtual ICollection<Product> Products { set; get; }
    }
}
