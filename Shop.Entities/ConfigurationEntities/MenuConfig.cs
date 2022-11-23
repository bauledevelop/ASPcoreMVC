using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities.ConfigurationEntities
{
    public class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");
            builder.HasKey(a => a.ID);
            builder.HasOne<Account>(f => f.Account).WithMany(a => a.Menus).HasForeignKey(f => f.CreatedBy).HasPrincipalKey(c => c.ID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
