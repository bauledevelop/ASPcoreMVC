using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities.Enities
{
    public class Menu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }
        [Column(TypeName = "nvarchar")]
        [StringLength(1024)]
        public string Name { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime UpdatedDate { set; get; }
        public long CreatedBy { set; get; }
        public bool Status { set; get; }
        [ForeignKey("CreatedBy")]
        public virtual Account Account { set; get; }
        public virtual ICollection<CategoryProduct> CategoryProducts { set; get; }
    }
}
