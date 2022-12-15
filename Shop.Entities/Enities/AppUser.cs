using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Entities.Enities
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(255)]
        public string Name { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime BirthDay { set; get; }
        [Column(TypeName ="nvarchar")]
        [StringLength(512)]
        public string Address { set; get; }
        public int Sex { set; get; }
        public int AccountType { set; get; }

    }
}
