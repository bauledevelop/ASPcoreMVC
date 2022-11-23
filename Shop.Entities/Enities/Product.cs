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
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }
        [Column(TypeName = "nvarchar")]
        [StringLength(1024)]
        public string Name { set; get; }
        [Column(TypeName = "nvarchar")]
        [StringLength(2048)]
        public string Description { set; get; }
        [Column(TypeName = "nvarchar")]
        [StringLength(2048)]
        public string Detail { set; get; }
        public long Price { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime UpdatedDate { set; get; }
        public bool IsDelete { set; get; }
        [DefaultValue(false)]
        public bool Status { set; get; }
        public long IDCategoryProduct { set; get; }
        public long CreatedBy { set; get; }
        [ForeignKey("CreatedBy")]
        public virtual Account Account { set; get; }
        [ForeignKey("IDCategoryProduct")]
        public virtual CategoryProduct CategoryProduct { set; get; }
        public virtual ICollection<File> Files { set; get; }
        public virtual ICollection<OrderDetail> OrderDetails { set; get; }
        public virtual ICollection<Comment> Comments { set; get; }
        public virtual ICollection<Rate> Rates { set; get; }
    }
}
