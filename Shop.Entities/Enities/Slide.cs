using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Entities.Enities
{
    public class Slide
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Content { set; get; }
        public DateTime CreatedDate { set; get; }
    }
}
