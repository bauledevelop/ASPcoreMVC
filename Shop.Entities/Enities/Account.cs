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
    public class Account 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Username { set; get; }
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Password { set; get; }
        [Column(TypeName =  "nvarchar")]
        [StringLength(1024)]
        public string Name { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime BirthDay { set; get; }
        public int Sex { set; get; }
        [Column(TypeName = "nvarchar")]
        [StringLength(2048)]
        public string Address { set; get; }
        
        [Column(TypeName = "varchar")]
        [StringLength(20)]
        public string Phone { set; get; }
        [Column(TypeName = "varchar")]
        [StringLength(2048)]
        public string Email { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { set; get; }
        public int AccountType { set; get; }
        public bool IsActive { set; get; }
        public bool IsDelete { set; get; }
        [DefaultValue(false)]
        public bool Status { set; get; }
        public virtual ICollection<Menu> Menus { set; get; }
        public virtual ICollection<Product> Products { set; get; }
        public virtual ICollection<CategoryProduct> CategoryProducts { set; get; }
        public virtual ICollection<File> Files { set; get; }
        public virtual ICollection<Comment> Comments { set; get; }
        public virtual ICollection<Order> Orders { set; get; }
        public virtual ICollection<Payment> Payments { set; get; }
        public virtual ICollection<Rate> Rates { set; get; }
    }
}
