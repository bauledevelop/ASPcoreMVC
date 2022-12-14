using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities.Enities
{
    public class Rate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }
        public int rate { set; get; }
        public long IDProduct { set; get; }
        public long IDAccount { set; get; }
        public virtual Product Product { set;get; }
        public virtual Account Account { set; get; }
    }
}
