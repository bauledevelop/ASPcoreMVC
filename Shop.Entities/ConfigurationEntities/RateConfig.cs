using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities.ConfigurationEntities
{
    public class RateConfig : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.ToTable("Rates");
            builder.HasKey(r => r.ID);
            builder.HasOne<Account>(f => f.Account).WithMany(a => a.Rates).HasForeignKey(f => f.IDAccount).HasPrincipalKey(c => c.ID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Product>(f => f.Product).WithMany(f => f.Rates).HasForeignKey(f => f.IDProduct).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
